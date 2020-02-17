using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActionController : MonoBehaviour
{
    public float gravity = 9.81f;

    public SteamVR_Action_Vector2 inputWalk;
    public SteamVR_Action_Boolean inputX;
    public SteamVR_Action_Boolean inputY;
    public SteamVR_Action_Boolean inputA;
    public SteamVR_Action_Boolean inputB;
    public float speed = 3;
    private Vector3 direction;

    CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {   
        // move direction
        direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputWalk.axis.x, 0, inputWalk.axis.y));
        // move operation
        // Vector3 movement = inputWalk.axis.magnitude > 0.15f ? Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime : Vector3.zero;

        XYControl();
        ABControl();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        character.Move( movement - (new Vector3(0, gravity, 0) * Time.deltaTime) );
    }

    /*
   // whole move operation
   // TODO: remove the glich while moving
    void Move()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputWalk.axis.x, 0, inputWalk.axis.y));
        Vector3 movement =  Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        character.Move(movement - new Vector3(0, gravity, 0));
    }
    //*/

    void XYControl()
    {
        if (inputX.stateDown)
        {
            Debug.Log("X shoot!");
        }
        if (inputY.stateDown)
        {
            Debug.Log("Y shoot!");
        }
    }

    void ABControl()
    {
        if (inputA.stateDown)
        {
            Debug.Log("A shoot!");
        }
        if (inputB.stateDown)
        {
            Debug.Log("B shoot!");
        }
    }
}
