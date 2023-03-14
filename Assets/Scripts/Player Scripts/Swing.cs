using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Player_Scripts
{
    public class Swing:MonoBehaviour
    {
        public float thresholdForSwing = 2f;
        public EventReference swingCut;
        public string loadBank;
        private int _cutCounter;
        private float _swingCooldown;
        
        private void Update() {
            if (_cutCounter > 10) {
                SceneManager.UnloadSceneAsync("VinesCutting");
                SceneManager.LoadScene("sensorTest");
            }
            // Get the acceleration and angular velocity of the phone
            Vector3 acceleration = Input.acceleration;

            // Check if the phone is swinging based on acceleration and angular velocity .. && angularVelocity.magnitude > thresholdForSwing
            if (acceleration.magnitude > thresholdForSwing) {
                float deltaTime = Time.time - _swingCooldown;
                if (deltaTime >= 1f) {
                    Debug.Log("Swinging detected!");
                    _swingCooldown = Time.time;
                    Vector3 tilt = Input.gyro.attitude.eulerAngles;
                    _cutCounter++;
                    Vector3 pos = transform.position;
                    if (tilt.z is < 220f and > 120f) {
                        pos.x +=.15f;
                    }else {
                        pos.x -= .15f;
                    }
                    pos.z += .5f;
                    RuntimeManager.PlayOneShot(swingCut, pos);
                }
            }
        }

        private void Start() {
            // Enable the gyroscope sensor
            RuntimeManager.LoadBank(loadBank);
            if (!SystemInfo.supportsGyroscope) 
                return;
            Input.gyro.enabled = true;
        }
        
        private void OnDestroy() {
            RuntimeManager.UnloadBank(loadBank);
        }
    }
}