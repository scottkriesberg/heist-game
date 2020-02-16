using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSight : MonoBehaviour
{

    public float viewAngle = 110f;
    public bool playerInSight;

    private GameObject player;
    private SphereCollider Col;
    private GuardState guardState;
    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //playerInSight = true;
        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            Debug.Log(angle);
            if (angle < viewAngle * 0.5f)
            {
                Debug.Log("GOT YOU");
                playerInSight = true;
            }
        }
    }
}
