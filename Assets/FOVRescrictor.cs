using UnityEngine;

public class FOVRescrictor : MonoBehaviour
{   
    [SerializeField]
    private float maxSize;
    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float minSpeed;

    private Camera head;
    private Vector3 oldPos;
    private Vector3 curPos;
    private GameObject restrictor;
    // private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        head = GetComponent<Camera>();
        restrictor = this.gameObject;
        restrictor.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        //Debug.Log("speed = " + velocity.magnitude);
        oldPos = curPos;
        restrictor.transform.localScale = AdjustRestrictor(velocity);
    }
    
    private Vector3 AdjustRestrictor(Vector3 velocity)
    {
        float exFOV = maxSize;
        float curSpeed = velocity.magnitude;
        if (curSpeed > minSpeed)
        {
            Debug.Log(velocity.magnitude);
            exFOV = ((maxSpeed - velocity.magnitude) / maxSpeed) * maxSize;
        }
        float newFOV = Mathf.Lerp(restrictor.transform.localScale[0], exFOV, 0.001f);
        return new Vector3(newFOV, newFOV, newFOV);
    }
}
