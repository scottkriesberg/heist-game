using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FadeControl : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("touch wall");
            SteamVR_Fade.Start(Color.black, 1);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log("touch wall");
            SteamVR_Fade.Start(Color.black, 0);
            //SteamVR_Fade.Start(Color.clear, 1);
        }
    }*/

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("leave the wall");
            SteamVR_Fade.Start(Color.clear, 1);
        }
    }
}
