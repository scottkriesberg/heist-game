using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Camera cameraToFollow;
    // Update is called once per frame
    void Update()
    {
        transform.position = cameraToFollow.transform.position;
    }
}
