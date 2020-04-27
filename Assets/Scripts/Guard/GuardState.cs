using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : MonoBehaviour
{
    public static bool GlobalToggle = false;

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

    public bool Caught { get; set; } = false;

    public void PauseGuard()
    {
        this.mState = AIState.rest;
    }

    public void WakeGuard()
    {
        this.mState = AIState.normal;
    }

    public GuardSight guardSight;
    // Start is called before the first frame update
    void Start()
    {
        this.mState = AIState.normal;
        guardSight = GetComponent<GuardSight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (guardSight.playerInSight && !this.Caught && GuardState.GlobalToggle)
        {
            this.mState = AIState.spotted;
            this.Caught = true;
            Alarm.Instance.StartAlarm("A Guard caught you trying to escape");
        }
    }

}
