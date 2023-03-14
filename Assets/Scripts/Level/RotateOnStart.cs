using UnityEngine;

namespace Level
{
    public class RotateOnStart : MonoBehaviour
    {
        private bool _set;
        // Start is called before the first frame update
        private void Start() {
            Input.compass.enabled = true;
        }

        private void Update() {
            if (_set)
                return;
            var currentHeading = Input.compass.magneticHeading;
            if (currentHeading == 0) 
                return;
            _set = true;
            transform.localRotation = Quaternion.Euler(0, currentHeading, 0);
        }
    }
}
