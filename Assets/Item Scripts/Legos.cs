using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legos : Throwable
{
    bool slow = false;

    void Update()
    {
        if (Thrown && !InAir)
        {
            slow = true;
            StartCoroutine("SlowDown");
        }
        else
            slow = false;
    }

    IEnumerator SlowDown()
    {
        Thrown = false;
        yield return new WaitForSeconds(5.0f);
    }
}
