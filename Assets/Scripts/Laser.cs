using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Laser : MonoBehaviour
{
    [SerializeField] [Range(0, 15f)]
    float length;
    [SerializeField] [Range(0, 20f)]
    float moveDistance;
    [SerializeField] [Range(0, 10f)]
    float moveSpeed;
    [SerializeField] [HideInInspector]
    private Vector3 endA;
    [SerializeField] [HideInInspector]
    private Vector3 endB;

    private float t;
    private float moveDir;
    private float invMoveDist;

    private void Start()
    {
        this.t = 0;
        this.moveDir = 1;
        if (this.moveDistance != 0) this.invMoveDist = 1 / this.moveDistance;
    }

    private void Update()
    {
        if (this.moveDistance <= 0) return;

        this.t += this.moveDir * this.moveSpeed * Time.deltaTime * this.invMoveDist;
        if (t >= 1)
        {
            t = 1;
            this.moveDir = -1;
        }
        else if (t <= 0)
        {
            t = 0;
            this.moveDir = 1;
        }

        this.transform.position = Vector3.Lerp(this.endA, this.endB, this.t);
    }

    private void OnValidate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();

        Vector3 start = Vector3.zero;
        Vector3 end = start + (Vector3.up * this.length);
        line.SetPositions(new Vector3[] { start, end });

        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        boxCollider.center = end * 0.5f;
        boxCollider.size = new Vector3(0.1f, end.y, 0.1f);
    }

    private void OnDrawGizmos()
    {
        Vector3 start = this.transform.position;
        Vector3 end = start + (this.transform.up * this.length);
        Gizmos.DrawLine(start, end);
    }

    private void OnDrawGizmosSelected()
    {
        if (this.moveDistance <= 0) return;
        Vector3 baseStart = this.endA;
        Vector3 baseEnd = this.endA + (this.transform.up * this.length);
        Gizmos.color = Color.blue;
        for (int i = 0; i < 10; i++)
        {
            Vector3 currStart = Vector3.Lerp(baseStart, baseEnd, ((float)i) / 10f);
            Vector3 currEnd = currStart + (this.transform.forward * this.moveDistance);
            Gizmos.DrawLine(currStart, currEnd);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.CauseDeath("You died from a laser", "laserScene");
        }
    }
}
