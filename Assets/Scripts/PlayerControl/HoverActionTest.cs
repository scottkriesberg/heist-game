using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HoverActionTest : MonoBehaviour
{
    private Interactable interactable;
    private GameObject objectInteractable;

    // Start is called before the first frame update
    private void Awake()
    {
        objectInteractable = transform.parent.gameObject;
        interactable = objectInteractable.GetComponent<Interactable>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnhandHoverBegin(Hand hand)
    {
        // interactable.highlightOnHover = true;
        // interactable.OnHandHoverBegin(hand);
        Debug.Log("Hover Begin2");
    }

    private void OnHandHoverEnd(Hand hand)
    {
        // interactable.highlightOnHover = false;
        // interactable.OnHandHoverEnd(hand);
        Debug.Log("Hover End2");
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
