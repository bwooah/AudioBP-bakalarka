using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerRotation : MonoBehaviour
{
    private float compassSmooth = 5f;
    private float m_lastMagneticHeading = 0f;
    private float m_lastSlerp = 0f;

    // private float updateInterval = 0.1f;
    private float smoothTime = 0.5f;
    private int rotationValidator = 0;
    private int valideRotationAfter = 3;
    private float elapsedTime = 0f;
    void Start(){
        // If you need an accurate heading to true north, 
        // start the location service so Unity can correct for local deviations:
        Input.location.Start();
        // Start the compass.
        Input.compass.enabled = true;
        // while (true)
        // {
        //     yield return new WaitForSeconds(updateI);
        //         
        //     float rawHeading = Input.compass.trueHeading;
        //     
        //     // Apply the low-pass filter to smooth out the compass readings
        //     m_lastMagneticHeading = (1 - smoothingFactor) * filteredHeading + compassSmooth * rawHeading;
        //
        //     // Rotate the object based on the filtered compass heading
        //     transform.rotation = Quaternion.Euler(0f, -m_lastMagneticHeading, 0f);
        // }
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        //do rotation based on compass
        float currentMagneticHeading = Input.compass.magneticHeading;
        // Debug.Log(currentMagneticHeading + " curread");
        if (m_lastMagneticHeading < (currentMagneticHeading - compassSmooth)%360 ||
            m_lastMagneticHeading > (currentMagneticHeading + compassSmooth)%360)
        {
            rotationValidator++;
            if (rotationValidator > valideRotationAfter)
            {
                rotationValidator = 0;
                m_lastMagneticHeading = currentMagneticHeading;
                m_lastSlerp = Mathf.Lerp(m_lastMagneticHeading, currentMagneticHeading, Mathf.Clamp01(elapsedTime / smoothTime));
                // Debug.Log(m_lastMagneticHeading + '\n');
                transform.localRotation = Quaternion.Euler(0, m_lastSlerp, 0);
            }
        }
        else
        {
            rotationValidator = 0;
        }
    }
}
