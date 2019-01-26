using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socks : Throwable
{
    bool stink = false;

    void Update()
    {
        if (Thrown && !InAir)
        {
            stink = true;
            StartCoroutine("StenchCloud");
        }
        else
            stink = false;
    }

    IEnumerator StenchCloud()
    {
        Thrown = false;
        yield return new WaitForSeconds(5.0f);
    }
}
