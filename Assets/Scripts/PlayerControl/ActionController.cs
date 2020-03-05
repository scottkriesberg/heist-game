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

    [SerializeField]
    CharacterController character;
    [SerializeField]
    Transform PlayArea;
    [SerializeField]
    Transform Head;

    private void Update()
    {
        Vector3 charToHead = this.Head.position - this.character.transform.position;
        charToHead = Vector3.ProjectOnPlane(charToHead, Vector3.up);
        this.character.Move(charToHead);

        float headY = this.Head.position.y * 0.5f;
        this.character.height = headY;
        this.character.center = Vector3.up * headY * 0.5f;

        XYControl();
        ABControl();
    }

    private void LateUpdate()
    {
        if ((this.character.collisionFlags & CollisionFlags.CollidedSides) != 0)
        {
            Vector3 headToChar = this.character.transform.position - this.Head.position;
            headToChar = Vector3.ProjectOnPlane(headToChar, Vector3.up);
            if (headToChar.magnitude < 0.05f) return;
            this.PlayArea.position += headToChar * 0.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move direction
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputWalk.axis.x, 0, inputWalk.axis.y));
        Vector3 movement = Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        PlayArea.transform.position += movement;
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
