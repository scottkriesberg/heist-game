using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActionController : MonoBehaviour
{
    public CharacterController character;
    public Transform playArea;

    public PlayerUI playerUI;

    public float gravityFactor = 9.81f;
    public SteamVR_Action_Vector2 inputWalk;
    public SteamVR_Action_Boolean inputX;
    public SteamVR_Action_Boolean inputY;
    public SteamVR_Action_Boolean inputA;
    public SteamVR_Action_Boolean inputB;
    public float speed = 3;

    private Vector3 direction;
    private Vector3 headToChar;
    private Vector3 charToHead;
    private Vector3 gravity;

    private Camera headSet;
    public bool noClip = false;

    private void Update()
    {
        // Finding move direction
        direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputWalk.axis.x, 0, inputWalk.axis.y));
        direction = Vector3.ProjectOnPlane(direction, Vector3.up);
        this.playArea.transform.position += direction * speed * Time.deltaTime;


        // Finding direction from char to head
        charToHead = headSet.transform.position - character.transform.position;
        //charToHead = Vector3.ProjectOnPlane(charToHead, Vector3.up);
        character.Move(charToHead);

        // Key Control
        XYControl();
        ABControl();
    }

    private void LateUpdate()
    {   // & CollisionFlags.CollidedBelow
        if (!this.noClip && (character.collisionFlags & CollisionFlags.CollidedSides) != 0)
        {
            headToChar = character.transform.position - headSet.transform.position;
            headToChar = Vector3.ProjectOnPlane(headToChar, Vector3.up);
            this.playArea.transform.position += headToChar;
        }
    }

    private void Awake()
    {
        this.headSet = this.GetComponentInChildren<Camera>();
        Debug.Assert(this.playArea != null);
        Debug.Assert(this.headSet != null);
    }

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
