using UnityEngine;

public class FOVRescrictor : MonoBehaviour
{   
    [SerializeField]
    private Vector3 defaultSize;
    [SerializeField]
    private Vector3 maxSize;
    [SerializeField]
    private Vector3 minSize;

    private Camera head;
    private Vector3 oldPos;
    private Vector3 curPos;
    private GameObject restrictor;
    // Start is called before the first frame update
    void Start()
    {
        head = GetComponent<Camera>();
        restrictor = this.gameObject;
        restrictor.transform.localScale = defaultSize;
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        Debug.Log("velocity = " + velocity);
        oldPos = curPos;
    }
    
    private void AdjustRestrictor()
    {

    }
}
