using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Item
{
    //Indicator if object is moving through the air
    [SerializeField]
    protected bool InAir;

    //Indicator if object has been thrown
    [SerializeField]
    protected bool Thrown;
    //Zero movement vector for comparison
    float stopped = .05f;

    //Shoots the item in the given angle with specified strength
    public void Throw (float strength, Vector2 direction)
    {
        Vector2 throwForce = direction * strength;
        GetComponent<Rigidbody2D>().AddForce(throwForce);
        Thrown = true;
    }

    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude <= stopped)
        {
            InAir = false;
            Thrown = false;
        }
        else
        {
            InAir = true;
        }
    }
}
