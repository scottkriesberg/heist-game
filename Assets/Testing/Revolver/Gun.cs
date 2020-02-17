using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;

    public Hand handToFire;

    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        Hand handStatus = interactable.attachedToHand;
        if (handStatus != null)
        {
            SteamVR_Input_Sources source = handStatus.handType;
            if (fireAction[source].stateDown)
            {
                Fire();
            }
        }
    }
    
    void Fire()
    {
        Debug.Log("Fire!!!");
    }
}
