using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
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

    public void RegisterSpawnPoint(PlayerSpawnPoint point)
    {
        if (this.currPlayer == null)
        {
            this.currPlayer = GameObject.Instantiate(this.playerPrefab);
            DontDestroyOnLoad(this.currPlayer.gameObject);
        }

        this.currPlayer.position = point.transform.position;
        this.currPlayer.rotation = point.transform.rotation;
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
}
