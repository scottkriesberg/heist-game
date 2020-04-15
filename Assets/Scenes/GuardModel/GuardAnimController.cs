using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private float cur;
    private float prev;
    private bool walk;

    // Start is called before the first frame update
    void Start()
    {
        walk = true;
        prev = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        cur = Time.time;
        if (cur - prev >= 5)
        {
            Debug.Log(walk);
            walk = !walk;
            prev = cur;
        }
        anim.SetBool("isWalking", walk);
    }
}
