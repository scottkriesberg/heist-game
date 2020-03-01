using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] [Range(0, 15f)]
    float length;
    [SerializeField] [Range(0, 150f)]
    float moveDistance;
    [SerializeField] [Range(0, 20f)]
    float moveSpeed;

    private float t;
    private float moveDir;
    private Vector3 endA;
    private Vector3 endB;
    private float invMoveDist;

    private void Start()
    {
        this.t = 0;
        this.moveDir = 1;
        this.endA = this.transform.position;
        this.endB = this.endA + (this.transform.forward * this.moveDistance);
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
}
