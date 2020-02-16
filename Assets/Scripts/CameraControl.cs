using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;

    public void SwitchCamera(int cameraID) {
        if(cameraID == currentCameraIndex) {
            return;
        }
        cameras[cameraID].enabled = true;
        cameras[currentCameraIndex].enabled = false;
        currentCameraIndex = cameraID;
        Debug.Log("Switched to camera" + cameraID);
    }

    // Start is called before the first frame update
    void Start()
    {
        cameras[0].enabled = true;
        for(int i = 1; i < cameras.Length; i++) {
            cameras[i].enabled = false;
        }
        currentCameraIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
