using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{

    const float totalDegrees = 120f;
    static float TimeInSec = 180f;
    float DegreePerSec = totalDegrees/ TimeInSec;
    public bool sunMoving = false;

    public void StartSunTimer()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // Timer for match.
        if (sunMoving)
        {
            TimeInSec -= Time.deltaTime;
            if (TimeInSec > 0)
            {
                transform.RotateAround(Vector3.up, Vector3.back, DegreePerSec * Time.deltaTime); //rotate sun at degreepersec rate each second
            }
        }
    }
}
