using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlledObjects : MonoBehaviour
{
    public abstract void Move(Vector2 Velocity);
    public abstract void Aim(Vector2 AimVector);
}

