using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGuardSight : MonoBehaviour
{

    public float viewAngle = 110f;
    public float viewRange = 20f;
    public bool playerInSight;

    private GameObject player;
    private SphereCollider Col;
    private GuardState guardState;

    private Vector3 toPlayer;

    Hacker panelCheck;

    // Start is called before the first frame update
    void Start()
    {
        Col = GetComponent<SphereCollider>();
        player = GameObject.Find("Celldoor_03");
        panelCheck = GameObject.Find("HackerCanvasPrefab/WM2000").GetComponent<Hacker>();
    }

    // Update is called once per frame
    void Update()
    {
        toPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(toPlayer.normalized, transform.forward);
        //Debug.Log(angle);
        playerInSight = false;

        // Debug.Log("panelCheck " + panelCheck.IsPanelOpen());
        if(!panelCheck.IsPanelOpen()) {
            return;
        }

        if (angle > viewAngle * 0.5f || toPlayer.magnitude > viewRange)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, toPlayer.normalized, out hit, 100f))
        {
            if (hit.collider.gameObject != player) return;
        }

        Debug.Log("GOT YOU");
        playerInSight = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //playerInSight = true;
        if (other.gameObject == player)
        {
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = playerInSight ? Color.green : Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + toPlayer);
    }
}
