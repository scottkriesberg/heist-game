using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyPointer : MonoBehaviour
{
    
    public float defaultLineLength = 5.0f;
    public GameObject m_LineEnd;
    public VRInputModule m_InputModule;

    private LineRenderer myLineRender;
    // Start is called before the first frame update
    private void Awake()
    {
        myLineRender = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default or new distance
        PointerEventData data = m_InputModule.GetData();
        float curLineLength = data.pointerCurrentRaycast.distance == 0 ? defaultLineLength : data.pointerCurrentRaycast.distance;

        // create the raycast 
        RaycastHit hit = CreateRayCast(curLineLength);

        // default position of the lineEnd
        Vector3 offset = transform.forward * curLineLength;
        Vector3 endPosition = transform.position + offset;

        // position of lineEnd if hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        // set the position of the lineEnd
        m_LineEnd.transform.position = endPosition;

        // set the position of the myLineRender
        myLineRender.SetPosition(0, transform.position);
        myLineRender.SetPosition(1, endPosition);

    }

    private RaycastHit CreateRayCast(float lineLength)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, lineLength);


        return hit;
    }
}
