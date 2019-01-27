using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DraggableObject : PlayerControlledObjects
{
    public bool editable = true;
    public Vector3 placedPosition;
    public Quaternion placedRotation;

    private Vector3 screenPoint;
    private Vector3 offset;
   // private ItemPlacer place = new ItemPlacer();
    public Rigidbody2D m_rigidBody;
    private float speedFactor;

    public override void Aim(Vector2 aimVector)
    {
        float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;
        transform.rotation = transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }

    public override void Move(Vector2 velocity) //should be called in FixedUpdate
    {
        //if it is possible to move then move according to the input
        m_rigidBody.AddForce(velocity * speedFactor);
    }

    public void Place() //places the thing
    {
        placedPosition = transform.position;
        placedRotation = transform.rotation;
    }

}
