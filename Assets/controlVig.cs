using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class controlVig : MonoBehaviour
{
    [SerializeField]
    private float FOVChangeRate = 1.0f;
    [SerializeField]
    Camera camera;
    [SerializeField]
    private float maxSpeed = 1.2f;
    [SerializeField]
    private float minSpeed = 0.8f;
    [SerializeField]
    private float maxFOV = 0.8f;
    [SerializeField]
    private PostProcessProfile camProfile;
    private Vector3 oldPos;
    private Vector3 curPos;


    // Start is called before the first frame update
    void Start()
    {
        oldPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FOVRestrict();
    }

    void FOVRestrict()
    {
        Vignette FOV = null;
        this.camProfile.TryGetSettings<Vignette>(out FOV);
        AdjustRestrictor(FOV);
    }

    private void AdjustRestrictor(Vignette FOV)
    {
        curPos = camera.transform.position;
        Vector3 velocity = (curPos - oldPos) / Time.deltaTime;
        oldPos = curPos;
        float exFOV = maxFOV;
        float curSpeed = velocity.magnitude;
        if (curSpeed < maxSpeed)
        {
            // Debug.Log("curSpeed = " + curSpeed);
            
            exFOV = (velocity.magnitude * FOVChangeRate / maxSpeed) * maxFOV;
            // Debug.Log(e)
        }
        FOV.intensity.value = Mathf.Lerp(FOV.intensity.value, exFOV, 0.005f);
    }
}
