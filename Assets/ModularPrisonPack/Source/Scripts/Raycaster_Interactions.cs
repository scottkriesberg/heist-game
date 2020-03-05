using UnityEngine;
using System.Collections;

public class Raycaster_Interactions : MonoBehaviour {
	
	Camera cam;
	[Range(1, 5)]
	public float rayDistance = 2f;
	public Texture2D crosshair, eButton;

	int crossHairStatus = 0;
    public bool showCrosshair;

    public AudioClip[] buzzers;
        
	void Start () {
		ConfigureCamera ();
		
	}

	private void ConfigureCamera() {
		cam = Camera.main;
		if (cam == null) {
			Debug.LogError ("Main camera tag not found in scene! No pre-coded interactions will work!");
			Destroy (this.gameObject);
		}
		if (cam.allowMSAA)
			cam.allowMSAA = false;
		if (!cam.allowHDR)
			cam.allowHDR = true;
		
	}
	
	void Update ()
	{
		Ray ray = cam.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.GetComponent<Door>())
            {
                crossHairStatus = 1;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<Door>().InteractWithThisDoor();
                }

            }
            else if (hit.transform.name.Equals("Maingate_Lower") || hit.transform.name.Equals("MainGate_Upper"))
            {
                crossHairStatus = 1;
                Animator a = hit.transform.parent.GetComponent<Animator>();
                if (a != null && Input.GetKeyDown(KeyCode.E))
                {
                    a.GetComponent<MainGate>().PlayGateAudio(a.GetBool("open"));
                    if (a.GetBool("open"))
                    {
                        a.SetBool("open", false);
                    }
                    else
                    {
                        a.SetBool("open", true);
                    }
                }
            }
            else if (hit.transform.GetComponent<SlidingDoor>())
            {
                crossHairStatus = 1;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    float f = Random.Range(0, buzzers.Length);
                    AudioSource.PlayClipAtPoint(buzzers[(int)f], hit.transform.position);
                    StartCoroutine(delayedOpen(1.5f, hit.transform.GetComponent<SlidingDoor>()));
                }
                    
                    //hit.transform.GetComponent<SlidingDoor>().InteractWithSlidingDoor();
            }
            else
            {
                crossHairStatus = 0;
            }
        }
        else {
            crossHairStatus = 0;
        }

        //showCrosshair = isAnimationPlaying () ? false : true;

		

	}

    IEnumerator delayedOpen(float secs, SlidingDoor sd) {
        yield return new WaitForSeconds(secs);
        sd.InteractWithSlidingDoor(!sd.isOpen);

    }

	void OnGUI() {
		if (showCrosshair) {
			switch (crossHairStatus) {
			case 0:
			//Draw default crosshair if integer set to 0
				Rect rect = new Rect (Screen.width / 2, Screen.height / 2, crosshair.width, crosshair.height);
				GUI.DrawTexture (rect, crosshair);
				break;
			case 1:
			//Draw E button sprite if integer set to 1 (object recognized by raycaster)
				Rect rect2 = new Rect ((Screen.width / 2) - eButton.width / 2, (Screen.height / 2) - eButton.height / 2, eButton.width, eButton.height);
				GUI.DrawTexture (rect2, eButton);
				break;
			}	  
		}
	}
}
