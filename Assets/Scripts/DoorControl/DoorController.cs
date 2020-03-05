using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float mappingChangeRate = 0.4f;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject keypad;
    [SerializeField] private string keyCard;
    [SerializeField] public string password;
    [SerializeField] private SlidingDoor slidingDoor;
    public Transform startPosition;
    public Transform endPosition;
    public float openTimer = 3.0f;

    // private string codeEntered;
    private float linearMappingValue;
    private bool passwordMatched;
    private bool keyPass;
    private bool fullOpen;  // flag that is set when the door is fully opened and the card key is not on the card sensor.
    private bool opening;
    private Transform doorTransform;
    private float currentTime;   // record the time starting from recieving the door open signal. Default = -1
    private float backCloseTime; // the time when the door needs to be closed


    private void Start()
    {
        resetTimerVariablesFlag();
        doorTransform = door.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == keyCard)
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
    /*void Update()
    {
        // SenseKeyPad();
        ProcessDoor();
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
    }*/

    void ProcessDoor()
    {
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

    // check if the code match the password. if so set the openning flag; otherwise, do nothing.
    // TODO: set up the communication
    public void SenseKeyPad()
    {
        string codeEntered = keypad.GetComponent<ButtonAction>().getCodeEntered();
        if (codeEntered.Equals(password) && CellConstants.buttonsUnlocked)
        {
            this.slidingDoor.InteractWithSlidingDoor(true);
            // Debug.Log("Password match!!!");
        } 
    }

    public bool passwordMatchedFlag()
    {
        return passwordMatched;
    }
    
    // maintain the door open duration. When it is not the time to close: update the timer; otherwise reset the timer variables and flags.
    void updateTimer()
    {
        if (currentTime < backCloseTime)
        {
            currentTime = Time.time;
            // Debug.Log("Current time = " + currentTime + ", TimeToClose = " + backCloseTime);
        } 
        else
        {
            // Debug.Log("Time closed = " + currentTime + ", TimeToClose = " + backCloseTime);
            resetTimerVariablesFlag();
        }
    }

    void resetTimerVariablesFlag()
    {
        currentTime = -1;
        backCloseTime = -1;
        fullOpen = false;
        opening = false;
        passwordMatched = false;
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
        return;
        if (mappingChangeRate != 0.0f)
        {
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
