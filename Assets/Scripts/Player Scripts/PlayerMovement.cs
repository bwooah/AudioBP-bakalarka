using UnityEngine;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    public float stepLength = .5f;
	public EventReference stepsSound;
	public EventReference goingBackNotice;
	public string loadBank;
	
	void Start()
    {
        FMODUnity.RuntimeManager.LoadBank(loadBank);
    }

    void Update(){
        // check for tap on mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // get the direction the player is looking
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;
            direction.Normalize();
			if (direction.z < -0.5)
            {	
				Debug.Log(direction.z);
				FMODUnity.RuntimeManager.PlayOneShot(goingBackNotice, transform.position);
                // Player is trying to move backwards, don't move
                return;
            }

			FMODUnity.RuntimeManager.PlayOneShot(stepsSound, transform.position);

            // move the player in the direction they are looking
            transform.position += direction * stepLength;
        }
    }

	void OnDestroy()
    {
        FMODUnity.RuntimeManager.UnloadBank(loadBank);
    }
}