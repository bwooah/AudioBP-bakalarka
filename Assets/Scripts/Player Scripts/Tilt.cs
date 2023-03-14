using UnityEngine;

namespace Player_Scripts
{
    public class Tilt:MonoBehaviour
    {
        private void Start()
        {
            Input.gyro.enabled = true;
        }

        private void Update()
        {
            var temp = Input.gyro.attitude;
            transform.rotation = temp;
            Debug.Log(temp);
        }
    }
}