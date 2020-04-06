using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ControlCameraVig : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume volume;

    private Vignette FOV;
    
    // Start is called before the first frame update
    void Start()
    {
        volume.GetComponent<PostProcessVolume>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
