using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVRenderer : MonoBehaviour
{
    private static CCTVRenderer instance = null;

    public static CCTVRenderer Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CCTV Renderer singleton not found");
            }

            return instance;
        }
    }

    [SerializeField]
    private Camera[] cctvCams;
    [SerializeField]
    private int currCamera = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple CCTV Renderer singletons found");
        }

        instance = this;

        this.SetCamera(this.currCamera);
    }

    public void SetCamera(int camIndex)
    {
        if (camIndex < 0 || camIndex > this.cctvCams.Length)
        {
            Debug.LogError("Camera index out of range: " + camIndex.ToString());
            return;
        }

        this.currCamera = camIndex;

        for (int i = 0; i < this.cctvCams.Length; i++)
        {
            this.cctvCams[i].enabled = i == currCamera;
        }
    }
}
