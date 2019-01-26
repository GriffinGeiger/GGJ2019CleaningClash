using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public virtual Item Pickup()
    {
        //Binds the object so that it floats in the air

        //Pick the object off of the ground
        transform.Translate(0, 1, 0);
        return this;
    }
}
