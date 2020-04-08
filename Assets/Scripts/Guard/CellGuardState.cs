using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGuardState : MonoBehaviour
{
    public enum AIState
    {
        rest,
        normal,
        spotted
    }

    [SerializeField]
    private Material mMat;

    private AIState _state;

    public AIState mState
    {
        get
        {
            return _state;
        }

        private set
        {
            _state = value;
            mMat.color = value == AIState.spotted ? Color.red : value == AIState.normal ? Color.yellow : Color.green;
        }

    }

    public void PauseGuard()
    {
        this.mState = AIState.rest;
    }

    public void WakeGuard()
    {
        this.mState = AIState.normal;
    }

    public CellGuardSight guardSight;
    // Start is called before the first frame update
    void Start()
    {
        this.mState = AIState.normal;
        guardSight = GetComponent<CellGuardSight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            this.mState = AIState.rest;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            this.mState = AIState.normal;
        }
        else if (Input.GetKey(KeyCode.Alpha3) || guardSight.playerInSight)
        {
            this.mState = AIState.spotted;
        }
    }

}

