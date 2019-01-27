using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : DraggableObject
{
    float angle;
    float range = Mathf.PI / 2;
    float flipVal = 0;
    bool powered = false;
    float force;
    float powerLevel = 0;
    float updater = .1f;

    //Using a battery to power up the fan
    void Power(Battery bat)
    {
        if (bat != null)
        {
            powered = true;
            force = 100;
            powerLevel = 100;
            Destroy(bat);
        }
        
    }

    void Start()
    {
        if (transform.position.x > Screen.width)
        {
            flipVal = Mathf.PI;
        }
    }

    void FixedUpdate()
    {
        //If the fan is powered up
        if (powered)
        {
            //Adjust oscillation direction
            if (Mathf.Abs(angle) - flipVal >= range)   
                updater *= -1;

            RaycastHit hit = new RaycastHit();
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, forward, out hit, 10))
            {
                Vector3 forceDirection = hit.transform.position - transform.position;
                hit.rigidbody.AddForceAtPosition(forceDirection, transform.position);
            }
            angle += updater;
            powerLevel -= 1;
            if (powerLevel <= 0)
                powered = false;
        }

        
    }
}
