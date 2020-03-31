using UnityEngine;

public class CustomAutoDoor : MonoBehaviour
{
    public float mappingChangeRate = 0.4f;
    [SerializeField] private GameObject door;
    public Transform startPosition;
    public Transform endPosition;
    public float openTimer = 3.0f;

    private float linearMappingValue;
    private bool keyPass;
    private bool fullOpen;
    private bool opening;
    private Transform doorTransform;
    private float currentTime;
    private float backCloseTime;


    private void Start()
    {
        fullOpen = false;
        opening = false;
        doorTransform = door.transform;
        currentTime = -1;
        backCloseTime = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Key in!");
        if (other.tag == "KeyCard")
        {
            currentTime = -1;
            backCloseTime = -1;
            keyPass = true;
            opening = true;
            Debug.Log("Key Enter!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        keyPass = false;
        Debug.Log("Key Out!");
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoorAction();
        if (opening)
        {
            if (door.transform.position == endPosition.position && !keyPass && currentTime == -1)
            {
                Debug.Log("Full Open");
                fullOpen = true;
                currentTime = Time.time;
                backCloseTime = currentTime + openTimer;
                Debug.Log("Time Full Open = " + currentTime + ", TimeToClose = " + backCloseTime);
            }
            if (!fullOpen)
            {
                OpenDoorAction();
            }
            else
            {
                updateTimer();
            }
        }
        else
        {
            if (!keyPass)
            {
                CloseDoorAction();
            }
        }
    }


    void updateTimer()
    {
        if (currentTime < backCloseTime)
        {
            currentTime = Time.time;
            Debug.Log("Current time = " + currentTime + ", TimeToClose = " + backCloseTime);
        }
        else
        {
            Debug.Log("Time closed = " + currentTime + ", TimeToClose = " + backCloseTime);
            currentTime = -1;
            backCloseTime = -1;
            fullOpen = false;
            opening = false;
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
            Debug.Log(start.position + " to " + end.position);
            Vector3 direction = end.position - start.position;
            float length = direction.magnitude;
            direction.Normalize();
            Vector3 displacement = doorTransform.position - start.position;
            linearMappingValue = Vector3.Dot(displacement, direction) / length;
            linearMappingValue = Mathf.Clamp01(linearMappingValue + (mappingChangeRate * Time.deltaTime));
            doorTransform.position = Vector3.Lerp(start.position, end.position, linearMappingValue);
        }
    }
}
