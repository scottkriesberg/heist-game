using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtons : MonoBehaviour
{
    // public GameObject someGameObject;
    public CameraControl cameraCtrl;

    public void ClickTest(int buttonID) {
        Debug.Log("Button" + buttonID + " clicked");
        cameraCtrl.SwitchCamera(buttonID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
