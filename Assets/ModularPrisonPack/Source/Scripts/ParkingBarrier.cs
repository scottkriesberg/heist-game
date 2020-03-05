using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingBarrier : MonoBehaviour {

    bool isPlayerInZone = false;
    Animator animator;
    [Range(1, 20)]
    public float closeDelayTime = 5f;

    private void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPlayerInZone) {
            if (Input.GetKeyDown(KeyCode.E) && !animator.GetBool("open")) {
                animator.SetBool("open", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player")) {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player")) {
            isPlayerInZone = false;
            StartCoroutine(delayedClose(closeDelayTime));
        }
    }

    IEnumerator delayedClose(float delay) {
        yield return new WaitForSeconds(delay);
        if (!isPlayerInZone) {
            animator.SetBool("open", false);
        }

    }


}
