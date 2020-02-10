using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActionController : MonoBehaviour
{
    public SteamVR_Action_Vector2 walkInput;
    public float speed = 1;
    public float gravity = 9.81f;
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
        direction = Player.instance.hmdTransform.TransformDirection(new Vector3(walkInput.axis.x, 0, walkInput.axis.y));
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // move operation
        Vector3 movement = Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        character.Move(movement - new Vector3(0, gravity, 0) * Time.deltaTime);
    }

   // whole move operation
    void Move()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(walkInput.axis.x, 0, walkInput.axis.y));
        Vector3 movement =  Vector3.ProjectOnPlane(direction, Vector3.up) * speed * Time.deltaTime;
        character.Move(movement - new Vector3(0, gravity, 0));
    }
}
