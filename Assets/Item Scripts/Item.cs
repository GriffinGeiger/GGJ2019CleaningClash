using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public virtual Item Pickup()
    {
        //Binds the object so that it floats in the air
        GetComponent<Rigidbody>().isKinematic = true;

        //Pick the object off of the ground
        transform.Translate(0, 10, 0);
        return this;
    }
}
