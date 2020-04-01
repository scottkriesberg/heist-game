using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActionController : MonoBehaviour
{
    [SerializeField] CharacterController character;
    [SerializeField] Camera headSet;
    [SerializeField] Player player;

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



    private void Update()
    {
        // Finding move direction
        direction = Player.instance.hmdTransform.TransformDirection(new Vector3(inputWalk.axis.x, 0, inputWalk.axis.y));

        // Finding direction from char to head
        charToHead = headSet.transform.position - character.transform.position;
        // headToChar = Vector3.ProjectOnPlane(headSet.transform.position - character.transform.position, Vector3.up);

        // Key Control
        XYControl();
        ABControl();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        // TODO: deal with gravity;
        gravity = new Vector3(0, gravityFactor, 0) * Time.deltaTime;
        player.transform.position += movement;
        // character.Move( movement - (new Vector3(0, gravity, 0) * Time.deltaTime) );
        // TODO: eliminate the offset
        character.Move(charToHead);
    }

    private void LateUpdate()
    {   // & CollisionFlags.CollidedBelow
        if ((character.collisionFlags & CollisionFlags.CollidedSides) != 0)
        {
            headToChar = character.transform.position - headSet.transform.position;
            player.transform.position += headToChar;
        }
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
