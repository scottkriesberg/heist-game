using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SlidingDoor : MonoBehaviour {

    const string OPEN = "open";

    public AudioClip openSound;
    public AudioClip closeSound;

    public bool isOpen = false;

    public void InteractWithSlidingDoor(bool open) {
        if (open == isOpen) return;
        isOpen = open;

        Animator a = GetComponent<Animator>();
        a.SetBool(OPEN, open);
        if (openSound != null || closeSound != null)
        {
            if (open)
                AudioSource.PlayClipAtPoint(openSound, a.transform.position);
            else
                AudioSource.PlayClipAtPoint(closeSound, a.transform.position);
        }
    }
}
