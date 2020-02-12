using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandAttach : MonoBehaviour
{
    private Interactable interactableObject;
    // Start is called before the first frame update
    void Start()
    {
        interactableObject = GetComponent<Interactable>();
    }

    private void OnhandHoverBegin(Hand hand)
    {
        Debug.Log("Hover Begin");
    }

    private void OnHandHoverEnd(Hand hand)
    {
        Debug.Log("Hover End");
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);
        if (interactableObject.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interactableObject);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactableObject);
        }
    }
}
