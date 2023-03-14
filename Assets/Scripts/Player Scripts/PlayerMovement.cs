using FMODUnity;
using UnityEngine;

namespace Player_Scripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public float stepLength = .5f;
		public EventReference stepsSound;
		public EventReference goingBackNotice;
		public string loadBank;
		public GameObject walkingTo;

		private void Start() {
			RuntimeManager.LoadBank(loadBank);
		}

		private void Update() {
			// check for tap on mobile
			if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Began) return;
			// get the direction the player is looking
			Vector3 position = transform.position;
			Vector3 direction = transform.forward;
			position += direction * stepLength;
			Vector3 goal = walkingTo.transform.position;
			// Debug.Log("audio source: " + goal + '\n' + "before move: " + direction + '\n' + "after move: "+ position);
			// Debug.Log(Vector3.Distance(position, goal));
			// Debug.Log(Vector3.Distance(transform.position, goal));
			if (Vector3.Distance(position, goal)>Vector3.Distance(transform.position, goal)) {
				//don't move backwards
				RuntimeManager.PlayOneShot(goingBackNotice, transform.position);
				return;
			}
			//play step sound-handled by fmod
			RuntimeManager.PlayOneShot(stepsSound, position);

			// move forward
			transform.position = position;
		}

		private void OnDestroy() {
			RuntimeManager.UnloadBank(loadBank);
		}
	}
}