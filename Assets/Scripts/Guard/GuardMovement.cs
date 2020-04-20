using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform[] patrolWayPoints;
    public float stoppingDistance = 0.1f;

    private int direction = 1;
    private GuardState guardState;
    private int wayPointIndex;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.guardState = this.GetComponent<GuardState>();
        wayPointIndex = 0;

    }

    public void Reset()
    {
        this.direction = 1;
        this.wayPointIndex = 0;
        this.transform.position = this.patrolWayPoints[this.wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (guardState.mState == GuardState.AIState.normal)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, patrolWayPoints[wayPointIndex].position, step);

            if(Vector3.Distance(transform.position, patrolWayPoints[wayPointIndex].position) < stoppingDistance)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                transform.LookAt(patrolWayPoints[wayPointIndex]);
            }
        }

        this.audioSource.volume = this.guardState.mState == GuardState.AIState.normal && this.moveSpeed > 0 ? 0.1f : 0f;
    }
}
