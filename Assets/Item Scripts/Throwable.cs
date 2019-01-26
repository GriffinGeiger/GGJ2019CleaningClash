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
    Vector2 stop = new Vector2(0, 0);

    //Shoots the item in the given angle with specified strength
    public void Throw (float strength, float angle)
    {
        Vector2 throwForce = new Vector2(Mathf.Cos(angle) * strength, Mathf.Sin(angle) * strength);
        Debug.Log("Throwing with a force of: " + throwForce);
        GetComponent<Rigidbody2D>().AddForce(throwForce);
        Thrown = true;
    }

    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity == stop)
        {
            InAir = false;
        }
        else
        {
            InAir = true;
        }
    }
}
