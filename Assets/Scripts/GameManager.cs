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
    private Vector3 oldPos;
    private Vector3 curPos;
    private Camera camera;
    private PostProcessVolume fovVolume;

    // .........................
    public static GameManager Instance;
    public static int laserLayer;

    [SerializeField]
    private PostProcessProfile camProfile;
    [SerializeField]
    private Transform playerPrefab;

    public Transform currPlayer;
    
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

        SceneManager.sceneLoaded += (a, b) => this.SetCameraMode(false);
    }
    void Update()
    {
        FOVRestrict();
    }

    public void RegisterSpawnPoint(PlayerSpawnPoint point)
    {
        if (this.currPlayer == null)
        {
            this.currPlayer = GameObject.Instantiate(this.playerPrefab);
            DontDestroyOnLoad(this.currPlayer.gameObject);
        }

        this.currPlayer.position = point.transform.position;
        this.currPlayer.rotation = point.transform.rotation;

        // find the camera
        this.camera = currPlayer.GetComponentInChildren<Camera>();
        this.fovVolume = currPlayer.GetComponentInChildren<PostProcessVolume>();
        fovVolume.isGlobal = true;
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
        Vignette FOV = null;
        this.fovProfile.TryGetSettings<Vignette>(out FOV);
        AdjustRestrictor(FOV);
    }

    private void AdjustRestrictor(Vignette FOV)
    {
        curPos = camera.transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        oldPos = curPos;
        float exFOV = maxFOV;
        float curSpeed = velocity.magnitude;
        if (curSpeed < maxSpeed)
        {
            Debug.Log("curSpeed = " + curSpeed);
            exFOV = Mathf.Max((velocity.magnitude * fovChangeRate / maxSpeed) * maxFOV, this.minFOV);
        }
        FOV.intensity.value = Mathf.Lerp(FOV.intensity.value, exFOV, 0.005f);
    }
}
