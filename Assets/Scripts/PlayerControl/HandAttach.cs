using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandAttach : MonoBehaviour
{
    private Interactable interactable;
    private GameObject objectInteractable;

    // Start is called before the first frame update
    private void Awake()
    {
        objectInteractable = transform.parent.gameObject;
        interactable = objectInteractable.GetComponent<Interactable>();
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
        bool isGrabEnding = hand.IsGrabEnding(objectInteractable);
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(objectInteractable, grabType);
            hand.HoverLock(interactable);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(objectInteractable);
            hand.HoverUnlock(interactable);
        }
    }
}
