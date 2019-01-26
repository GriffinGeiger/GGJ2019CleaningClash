using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Item
{
    //Indicator if object is moving through the air
    protected bool InAir;

    //Indicator if object has been thrown
    protected bool Thrown;
    //Zero movement vector for comparison
    Vector3 stop = new Vector3(0, 0, 0);

    //Shoots the item in the given angle with specified strength
    void Throw (float strength, Vector2 angle)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().AddForce(angle * strength);
        Thrown = true;
    }

    void Update()
    {
        if (GetComponent<Rigidbody>().velocity == stop)
        {
            InAir = false;
        }
        else
        {
            InAir = true;
        }
    }
}
