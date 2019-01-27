using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{

    const float totalDegrees = 120f;
    static float TimeInSec = 180f;
    float DegreePerSec = totalDegrees/ TimeInSec;

    public void StartSunTimer()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // Timer for match.
        TimeInSec -= Time.deltaTime;
        if (TimeInSec > 0) { 

            transform.RotateAround(Vector3.up, Vector3.back, DegreePerSec * Time.deltaTime);
        }
    }
}
