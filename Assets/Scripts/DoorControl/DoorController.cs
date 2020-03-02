using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float mappingChangeRate = 0.4f;
    [SerializeField] private GameObject door;
    [SerializeField] private SlidingDoor slidingDoor;
    [SerializeField] private GameObject keypad;
    [SerializeField] private string keyCard;
    [SerializeField] private bool autoClose;
    [SerializeField] private string password; // changed to private
    
    public Transform startPosition;
    public Transform endPosition;
    public float openTime = 3.0f;

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
        ResetTimerVariablesFlag();
        doorTransform = door.transform;
    }

    void Update()
    {
        if (currentTime != -1) 
        {
            UpdateTimer();
        }
        //SenseKeyPad();
        //ProcessDoor();
        /*if (opening)
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
        }*/
    }

    // sense the key card
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

    // check if the code match the password. if so set the openning flag; otherwise, do nothing.
    // TODO: set up the communication
    public void SenseKeyPad()
    {
        // read the entered code from the key pad
        string codeEntered = keypad.GetComponent<ButtonAction>().getCodeEntered();
        if (codeEntered.Equals(password))
        {
            this.slidingDoor.InteractWithSlidingDoor(true);
            // timer function
            if (autoClose)
            {
                this.currentTime = Time.time;
                this.backCloseTime = currentTime + openTime;
            }
        } 
    }

    public bool PasswordMatchedFlag()
    {
        return passwordMatched;
    }
    
    void ResetTimerVariablesFlag()
    {
        currentTime = -1;
        backCloseTime = -1;
        fullOpen = false;
        opening = false;
        passwordMatched = false;
    }

    /*void ProcessDoor()
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
    }*/

    // maintain the door open duration. When it is not the time to close: update the timer; otherwise reset the timer variables and flags.
    void UpdateTimer()
    {
        if (currentTime < backCloseTime)
        {
            currentTime = Time.time;
        }
        else
        {
            CloseDoorAction();
            ResetTimerVariablesFlag();
        }
    }

    /*void OpenDoorAction()
    {
        DoorGoTo(startPosition, endPosition);
    }*/

    void CloseDoorAction()
    {
        // DoorGoTo(endPosition, startPosition);
        this.slidingDoor.InteractWithSlidingDoor(false);
    }

    /* void DoorGoTo(Transform start, Transform end)
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
     }*/
}
