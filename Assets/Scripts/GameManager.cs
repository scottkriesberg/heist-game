using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int PlayerLayer;
    public static bool CurrFailed = false;

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
    private MyScene[] scenes; // 0 = status scene, 1 = cell, 2 = laser, 3 = guard
    [SerializeField]
    private TMPro.TextMeshPro playerStatusText;
    [SerializeField]
    private TMPro.TextMeshProUGUI playerStatusTextUI;
    [SerializeField]
    private Button[] levelButtons;

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

    private Coroutine playerPlaceCoroutine;
    private Coroutine levelSwitchRoutine;

    public int CurrScene { get => this.currLoadedScene; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        PlayerLayer = LayerMask.NameToLayer("Player");
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
    }

    private void Start()
    {
        foreach (MyScene scene in this.scenes)
        {
            if (scene.gameObject.activeSelf) scene.gameObject.SetActive(false);
        }

        PlayerPrefs.SetInt("maxAvailableLevel", 3);
        this.levelSwitchRoutine = this.StartCoroutine(this.SwitchLevelRoutine("Welcome To PrisonBreakVR", 0));
    }

    private void UnloadCurrScene()
    {
        if (this.currLoadedScene < 0 || this.currLoadedScene >= this.scenes.Length) return;

        this.scenes[this.currLoadedScene].OnUnload();
        this.scenes[this.currLoadedScene].gameObject.SetActive(false);
    }

    private void LoadScene(int i)
    {
        if (i < 0 || i >= this.scenes.Length) return;

        this.scenes[i].gameObject.SetActive(true);
        this.scenes[i].OnLoad();

        this.currLoadedScene = i;
    }

    public void StartLevelClicked(int level)
    {
        if (this.levelSwitchRoutine != null) this.StopCoroutine(this.levelSwitchRoutine);
        this.levelSwitchRoutine = this.StartCoroutine(this.SwitchLevelRoutine("Welcome To PrisonBreakVR", level));
    }

    public Camera GetPlayerCamera()
    {
        return this.playerCamera;
    }

    void Update()
    {
        FOVRestrict();
    }

    public void RegisterSpawnPoint(PlayerSpawnPoint point)
    {
        if (this.playerPlaceCoroutine != null) this.StopCoroutine(this.playerPlaceCoroutine);
        this.playerPlaceCoroutine = this.StartCoroutine(this.PlacePlayer(point));
    }

    private IEnumerator PlacePlayer(PlayerSpawnPoint point)
    {
        this.currPlayer.noClip = true;
        this.currPlayer.VRIK.enabled = false;

        Vector3 playAreaToPlayer = this.playerCamera.transform.position - this.currPlayer.playArea.position;
        playAreaToPlayer = Vector3.ProjectOnPlane(playAreaToPlayer, Vector3.up);
        this.currPlayer.playArea.position = point.transform.position - playAreaToPlayer;
        this.currPlayer.playArea.rotation = point.transform.rotation;
        this.currPlayer.character.transform.position = point.transform.position;
        this.currPlayer.VRIK.transform.position = point.transform.position;

        yield return new WaitForEndOfFrame();

        this.currPlayer.noClip = false;
        this.currPlayer.VRIK.enabled = true;
    }

    public void SetCameraMode(bool infrared)
    {
        ColorGrading cg = null;
        this.camProfile.TryGetSettings<ColorGrading>(out cg);
        cg.colorFilter.value = infrared ? Color.green : Color.white;

        if (CameraControl.instance == null) return;

        foreach (Camera cam in CameraControl.instance.cameras)
        {
            if (infrared) cam.cullingMask = cam.cullingMask | (1 << laserLayer);
            else cam.cullingMask = cam.cullingMask & (~0 ^ (1 << laserLayer));
        }
    }

    public void SetPlayerHeadSmall(bool isSmall)
    {
        this.currPlayer.character.radius = isSmall ? 0.075f : 0.15f;
    }

    void FOVRestrict()
    {
        if (this.fovProfile == null) return;
        Vignette FOV = null;
        this.fovProfile.TryGetSettings<Vignette>(out FOV);
        AdjustRestrictor(FOV);
    }

    private void SetPlayerStatusText(string text)
    {
        this.playerStatusText.text = text;
        this.playerStatusTextUI.text = text;
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

    public void LevelFailed(string reason)
    {
        CurrFailed = true;
        this.levelSwitchRoutine = this.StartCoroutine(this.SwitchLevelRoutine(reason, 0, 3f));
    }

    public void LevelPassed(string passText)
    {
        if (CurrFailed) return;
        
        GuardState.GlobalToggle = false;
        this.levelSwitchRoutine = this.StartCoroutine(this.SwitchLevelRoutine(passText, 0));
    }

    private IEnumerator SwitchLevelRoutine(string welcomeText, int sceneToLoad, float delay = 0f)
    {
        bool isWaitScene = sceneToLoad == 0;
        const float invFadeTime = 1/2f;
        const float waitBlackTime = 2f;

        ColorGrading cg = null;
        this.fovProfile.TryGetSettings<ColorGrading>(out cg);
        cg.colorFilter.value = Color.white;

        if (delay > 0f) yield return new WaitForSeconds(delay);

        // fade to black
        {
            float t = 0f;
            while (t <= 1f)
            {
                cg.colorFilter.value = Color.Lerp(Color.white, Color.black, t);
                t += Time.deltaTime * invFadeTime;
                yield return new WaitForEndOfFrame();
            }

            cg.colorFilter.value = Color.black;
        }

        // switch scenes
        this.UnloadCurrScene();
        this.LoadScene(sceneToLoad);

        this.SetPlayerStatusText(welcomeText);
        if (isWaitScene)
        {
            int maxAvailableLevel = PlayerPrefs.GetInt("maxAvailableLevel");
            for (int i = 0; i < this.levelButtons.Length; i++)
            {
                this.levelButtons[i].interactable = i < maxAvailableLevel;
            }
        }

        // wait in black screen
        if (waitBlackTime > 0) yield return new WaitForSeconds(waitBlackTime);

        // fade in
        {
            float t = 0f;
            while (t <= 1f)
            {
                cg.colorFilter.value = Color.Lerp(Color.black, Color.white, t);
                t += Time.deltaTime * invFadeTime * 2f;
                yield return new WaitForEndOfFrame();
            }

            cg.colorFilter.value = Color.white;
        }

        yield return null;
    }
}
