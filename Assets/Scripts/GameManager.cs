using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    // For FOV
    [SerializeField]
    private float fovChangeRate = 3.0f;
    [SerializeField]
    private float maxSpeed = 1.2f;
    [SerializeField]
    private float minSpeed = 0.8f;
    [SerializeField]
    private float maxFOV = 0.8f;
    [SerializeField]
    private float minFOV = 0.3f;
    [SerializeField]
    private PostProcessProfile fovProfile;
    [SerializeField]
    private MyScene[] scenes; // 0 = cell, 1 = laser, 2 = guard
    [SerializeField] [Range(0, 2)]
    private int firstSceneToLoad = -1;

    private int currLoadedScene = -1;

    private Vector3 oldPos;
    private Vector3 curPos;
    private Camera playerCamera;
    private PostProcessVolume fovVolume;

    // .........................
    public static GameManager Instance;
    public static int laserLayer;

    [SerializeField]
    private PostProcessProfile camProfile;
    [SerializeField]
    private ActionController playerPrefab;

    public ActionController currPlayer;
    private string reasonOfLoss = "NULL";
    private string sceneToLoad;

    private Coroutine playerTextRoutine;
    private Coroutine playerPlaceCoroutine;
    private Coroutine loadSceneRoutine;

    public int CurrScene { get => this.currLoadedScene; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        laserLayer = LayerMask.NameToLayer("Laser");

        if (this.currPlayer == null)
        {
            this.currPlayer = GameObject.Instantiate(this.playerPrefab);
            this.playerCamera = currPlayer.GetComponentInChildren<Camera>();
            this.fovVolume = currPlayer.GetComponentInChildren<PostProcessVolume>();
            DontDestroyOnLoad(this.currPlayer.gameObject);
        }

        foreach (MyScene scene in this.scenes)
        {
            if (scene.gameObject.activeSelf) scene.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (this.firstSceneToLoad > -1)
        {
            this.loadSceneRoutine = this.StartCoroutine(this.LoadScene(this.firstSceneToLoad, 0f));
        }
    }

    private IEnumerator LoadScene(int i, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (i >= 0 && i < this.scenes.Length)
        {
            // todo: fade to black

            // unload current
            if (this.currLoadedScene > -1 && this.currLoadedScene < this.scenes.Length)
            {
                this.scenes[this.currLoadedScene].OnUnload();
                this.scenes[this.currLoadedScene].gameObject.SetActive(false);
            }

            // load new
            this.scenes[i].gameObject.SetActive(true);
            this.scenes[i].OnLoad();

            this.currLoadedScene = i;
        }

        yield return null;
    }

    public Camera GetPlayerCamera()
    {
        return this.playerCamera;
    }

    private void SetPlayerStatusText(string text)
    {
        this.currPlayer.GetComponent<ActionController>().playerUI.statusText.text = text;
    }

    void Update()
    {
        FOVRestrict();
    }

    public void RegisterSpawnPoint(PlayerSpawnPoint point)
    {
        Debug.Log("setting pos");

        Vector3 playAreaToPlayer = this.playerCamera.transform.position - this.currPlayer.playArea.position;
        playAreaToPlayer = Vector3.ProjectOnPlane(playAreaToPlayer, Vector3.up);

        if (this.playerPlaceCoroutine != null) this.StopCoroutine(this.playerPlaceCoroutine);
        this.currPlayer.noClip = true;
        this.currPlayer.playArea.position = point.transform.position - playAreaToPlayer;
        this.currPlayer.playArea.rotation = point.transform.rotation;
        this.currPlayer.character.transform.position = point.transform.position;
        this.StartCoroutine(this.ResetPlayerClipping());

        // find the camera
        //fovVolume.isGlobal = true;
    }

    public void SetCameraMode(bool infrared)
    {
        ColorGrading cg = null;
        this.camProfile.TryGetSettings<ColorGrading>(out cg);
        cg.colorFilter.value = infrared ? Color.green : Color.white;
        
        foreach (Camera cam in CameraControl.instance.cameras)
        {
            if (infrared) cam.cullingMask = cam.cullingMask | (1 << laserLayer);
            else cam.cullingMask = cam.cullingMask & (~0 ^ (1 << laserLayer));
        }
    }

    void FOVRestrict()
    {
        if (this.fovProfile == null) return;
        Vignette FOV = null;
        this.fovProfile.TryGetSettings<Vignette>(out FOV);
        AdjustRestrictor(FOV);
    }

    private void AdjustRestrictor(Vignette FOV)
    {
        curPos = playerCamera.transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        oldPos = curPos;
        float exFOV = maxFOV;
        float curSpeed = velocity.magnitude;
        if (curSpeed < maxSpeed)
        {
            //Debug.Log("curSpeed = " + curSpeed);
            exFOV = Mathf.Max((velocity.magnitude * fovChangeRate / maxSpeed) * maxFOV, this.minFOV);
        }
        FOV.intensity.value = Mathf.Lerp(FOV.intensity.value, exFOV, 0.005f);
    }

    public void CauseDeath(string reason, int sceneToLoad, float delay = 0f)
    {
        if (this.playerTextRoutine != null) this.StopCoroutine(this.playerTextRoutine);
        this.SetPlayerStatusText(reason);
        this.playerTextRoutine = this.StartCoroutine(this.ResetPlayerText());

        if (this.loadSceneRoutine != null) this.StopCoroutine(this.loadSceneRoutine);
        this.loadSceneRoutine = this.StartCoroutine(this.LoadScene(sceneToLoad, delay));
    }

    private IEnumerator ResetPlayerText()
    {
        yield return new WaitForSeconds(10f);
        this.SetPlayerStatusText("status");
        yield return null;
    }

    private IEnumerator ResetPlayerClipping()
    {
        yield return new WaitForSeconds(1);
        this.currPlayer.noClip = false;
        yield return null;
    }
}
