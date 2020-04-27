using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGuardMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform[] patrolWayPoints;
    public float stoppingDistance = 0.1f;

    private int direction = 1;
    private CellGuardState guardState;
    private int wayPointIndex;
    
    void Awake()
    {
        guardState = GetComponent<CellGuardState>();
        wayPointIndex = 0;

    }

    public void Reset()
    {
        this.direction = 1;
        this.wayPointIndex = 0;
        this.transform.position = this.patrolWayPoints[this.wayPointIndex].position;
        this.guardState.mState = CellGuardState.AIState.normal;
        this.guardState.Caught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (guardState.mState == CellGuardState.AIState.normal)
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
    }
}

