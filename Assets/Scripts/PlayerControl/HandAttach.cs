using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandAttach : MonoBehaviour
{
    private Interactable interactable;
    public GameObject interactableObject = null;

    // Start is called before the first frame update
    private void Awake()
    {
        interactable = interactableObject.GetComponent<Interactable>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnhandHoverBegin(Hand hand)
    {
        // interactable.highlightOnHover = true;
        // interactable.OnHandHoverBegin(hand);
        Debug.Log("Hover Begin");
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // interactable.highlightOnHover = false;
        // interactable.OnHandHoverEnd(hand);
        Debug.Log("Hover End");
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(interactableObject);
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(interactableObject, grabType);
            hand.HoverLock(interactable);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(interactableObject);
            hand.HoverUnlock(interactable);
        }
    }
}
