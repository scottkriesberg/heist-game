using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private int direction = 1;
    private GuardState guardState;

    // Start is called before the first frame update
    void Start()
    {
        guardState = GetComponent<GuardState>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (guardState.mState == GuardState.AIState.normal)
        {
            if (transform.position.z <= -20 || transform.position.z >= 20)
            {
                direction = -direction;
                transform.Translate(Vector3.back * direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.back * direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}
