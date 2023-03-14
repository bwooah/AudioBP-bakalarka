using TMPro;
using UnityEngine;

namespace Test_Scripts
{
    
    public class EditText : MonoBehaviour
    {
        private float _compassSmooth = 5f;
        private float m_lastMagneticHeading = 0f;
        private float m_lastSlerp = 0f;
        private float smoothTime = 0.7f;
        private int rotationValidator = 0;
        private int valideRotationAfter = 3;
        private float elapsedTime = 0f;
        private float lastMagneticHeading = 0f;
        public TMP_Text compasNoSmooth;
        
        public float smoothingFactor = 0.1f;
        public int medianFilterSize = 15;

        private float[] headingBuffer;
        private int headingIndex;
        private float filteredHeading;

        private void Start()
        {
            Input.location.Start();
            Input.compass.enabled = true;
            compasNoSmooth.text = "Jozo, pozdravujem ta!";
            
            headingBuffer = new float[medianFilterSize];

            // Fill buffer with initial heading
            var initialHeading = Input.compass.magneticHeading;
            for (var i = 0; i < medianFilterSize; i++)
            {
                headingBuffer[i] = initialHeading;
            }
        }
        
        private void Update()
        {
            elapsedTime += Time.deltaTime;
            
            var currentMagneticHeading = Input.compass.magneticHeading;
            
            headingBuffer[headingIndex] = Input.compass.magneticHeading;
            headingIndex = (headingIndex + 1) % medianFilterSize;

            // Calculate the median of the heading buffer
            float[] sortedBuffer = (float[])headingBuffer.Clone();
            System.Array.Sort(sortedBuffer);
            float medianHeading = sortedBuffer[medianFilterSize / 2];

            // Smooth the median heading value over time
            filteredHeading = Mathf.Lerp(filteredHeading, medianHeading, Time.deltaTime * smoothingFactor);

            
            if (m_lastMagneticHeading < (currentMagneticHeading - _compassSmooth)%360 ||
                m_lastMagneticHeading > (currentMagneticHeading + _compassSmooth)%360)
            {
                rotationValidator++;
                if (rotationValidator > valideRotationAfter)
                {
                    rotationValidator = 0;
                    m_lastMagneticHeading = currentMagneticHeading;
                    m_lastSlerp = Mathf.Lerp(m_lastMagneticHeading, currentMagneticHeading, 
                        Time.deltaTime * smoothTime);
                }
            }
            else
            {
                rotationValidator = 0;
            }

            compasNoSmooth.text = "elapsed time: " + elapsedTime + '\n' + "current mag heading: " +
                                  currentMagneticHeading + '\n' + "my smooth mag heading: "
                                  + m_lastMagneticHeading + '\n' + "lerped myHeading:           " + m_lastSlerp
                                  + '\n' + "median filter:                  " + medianHeading;
            lastMagneticHeading = currentMagneticHeading;
        }
    }
}
