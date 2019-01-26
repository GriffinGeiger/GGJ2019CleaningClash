using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    float DegreePerSec = 90.0f / 180.0f;


    // Update is called once per frame
    void Update() =>
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(Vector3.zero, Vector3.back, DegreePerSec * Time.deltaTime);
}
