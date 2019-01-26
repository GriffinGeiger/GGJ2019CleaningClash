using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    static float TimeInSec = 180f;
    float DegreePerSec = 90.0f / TimeInSec;


    // Update is called once per frame
    void Update()
    {
        // Timer for match.
        TimeInSec -= Time.deltaTime;
        if (TimeInSec > 0) { 
            // Spin the object around the world origin at 20 degrees/second.
            transform.RotateAround(Vector3.zero, Vector3.back, DegreePerSec * Time.deltaTime);
        }
    }
}
