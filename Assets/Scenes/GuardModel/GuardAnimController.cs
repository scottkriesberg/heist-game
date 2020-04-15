using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private Vector3 oldPos;
    private Vector3 curPos;
    private bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curPos = this.transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        oldPos = curPos;
        if (velocity.magnitude > 3)
        {
            anim.SetBool("isWalking", true);
        } else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
