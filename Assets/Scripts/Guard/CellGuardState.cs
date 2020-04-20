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

        set
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
        if (guardSight.playerInSight)
        {
            this.mState = AIState.spotted;
            Alarm.Instance.StartAlarm("A Guard saw you hacking into the panel");
        }
    }

}

