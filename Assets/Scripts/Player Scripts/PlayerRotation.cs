using UnityEngine;

namespace Player_Scripts
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float compassSmooth = 5f;

        private float _lastHeading;
        private int _rotationValidator;
        private const int ValideRotationAfter = 3;

        // median filter 
        private float[] _headingBuffer;
        private int _bufferIndex;
        public int medianFilterSize = 15;

        private void Start(){
            // Start the compass.
            Input.compass.enabled = true;
            
            _headingBuffer = new float[medianFilterSize];
            var initialHeading = Input.compass.magneticHeading;
            for (var i = 0; i < medianFilterSize; i++)
                _headingBuffer[i] = initialHeading;
        }

        private void Update(){
            _headingBuffer[_bufferIndex] = Input.compass.magneticHeading;
            _bufferIndex = (_bufferIndex + 1) % medianFilterSize;
            
            // Calculate the median of the heading buffer
            var sortedBuffer = (float[])_headingBuffer.Clone();
            System.Array.Sort(sortedBuffer);
            var currentMagneticHeading = sortedBuffer[medianFilterSize / 2];
            
            //do rotation based on compass
            if (_lastHeading < (currentMagneticHeading - compassSmooth)%360 ||
                _lastHeading > (currentMagneticHeading + compassSmooth)%360) {
                _rotationValidator++;
                if (_rotationValidator <= ValideRotationAfter) return;
                _rotationValidator = 0;
                _lastHeading = currentMagneticHeading;
                transform.localRotation = Quaternion.Euler(0, _lastHeading, 0);
            }
            else
                _rotationValidator = 0;
        }
    }
}
