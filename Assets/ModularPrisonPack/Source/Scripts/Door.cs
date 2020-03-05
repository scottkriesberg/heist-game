using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour {

	public enum rotOrient
	{
		Y_Axis_Up,
		Z_Axis_Up,
		X_Axis_Up
	}

	public enum rotFixAxis
	{
		Y, 
		Z
	}

    public enum doorType
    {
        Regular,
        Sliding
    }

    public doorType doorMovement;
	public rotOrient rotationOrientation;
	public bool applyRotationFix = false;
	public rotFixAxis rotationAxisFix;
	public float doorOpenAngle = -90.0f;
	[Range(1,5)] public float speed = 3.0f;

	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;

	Quaternion doorOpen = Quaternion.identity;
	Quaternion doorClosed = Quaternion.identity;

	bool doorStatus = false;

	void Start() {
		if (this.gameObject.isStatic) {
			Debug.Log ("This door has been set to static and won't be openable. Doorscript has been removed.");
			Destroy (this);
		}
		switch (rotationOrientation) {
		case rotOrient.Z_Axis_Up:
			doorOpen = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + doorOpenAngle);
			break;
		case rotOrient.Y_Axis_Up:
			doorOpen = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y + doorOpenAngle, transform.localEulerAngles.z);
			break;
		case rotOrient.X_Axis_Up:
			if (!applyRotationFix) {
				doorOpen = Quaternion.Euler (transform.localEulerAngles.x + doorOpenAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
			} else {
				{
					if (rotationAxisFix.Equals (rotFixAxis.Y)) {
						doorOpen = Quaternion.Euler (transform.localEulerAngles.x + 90, 90f, 270f);
					} else if (rotationAxisFix.Equals (rotFixAxis.Z)) {
						doorOpen = Quaternion.Euler (transform.localEulerAngles.x + 90, 270f, 90f);
					}
				}
			}
			break;
		}
		doorClosed = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

	}

    public void InteractWithThisDoor() {
        switch (doorMovement) {
            case doorType.Regular:
                if (doorStatus) {
                    StartCoroutine(this.moveDoor(doorClosed));
                    if (doorCloseSound != null) {
                        StartCoroutine(delayedCloseAudio(speed / 50f));
                    }
                } else {
                    StartCoroutine(this.moveDoor(doorOpen));
                    if (doorOpenSound != null) {
                        AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
                    }
                }
                break;
            case doorType.Sliding:
                break;
    }
	}

	IEnumerator delayedCloseAudio(float delay){
		yield return new WaitForSeconds (delay);
		AudioSource.PlayClipAtPoint (doorCloseSound, this.transform.position);
	}

	IEnumerator moveDoor(Quaternion target) {
		while (Quaternion.Angle (transform.localRotation, target) > 0.5f) {
			transform.localRotation = Quaternion.Slerp (transform.localRotation, target, Time.deltaTime * speed);
			yield return null;
		}
		doorStatus = !doorStatus;
		yield return null;
	}
}



