using UnityEngine;

public class AutoOpen : MonoBehaviour
{
    public float mappingChangeRate = 0.4f;
    public Transform startPosition;
    public Transform endPosition;
    public float openTimer;

    private float linearMappingValue;
    private bool doorOpen;


    private void Start()
    {
        doorOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KeyCard")
        {
            doorOpen = true;
            Debug.Log("Key Enter!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (doorOpen)
        {
            doorOpen = false;
            Debug.Log("Key Out!");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            OpenDoorAction();
        }
        else
        {
            CloseDoorAction();
        }
    }

    void OpenDoorAction()
    {
        doorGoTo(startPosition, endPosition);
    }

    void CloseDoorAction()
    {
        doorGoTo(endPosition, startPosition);
    }

    void doorGoTo(Transform start, Transform end)
    {
        if (mappingChangeRate != 0.0f)
        {
            Vector3 direction = end.position - start.position;
            float length = direction.magnitude;
            direction.Normalize();
            Vector3 displacement = transform.position - start.position;
            linearMappingValue = Vector3.Dot(displacement, direction) / length;
            linearMappingValue = Mathf.Clamp01(linearMappingValue + (mappingChangeRate * Time.deltaTime));
            transform.position = Vector3.Lerp(start.position, end.position, linearMappingValue);
        }
    }
}
