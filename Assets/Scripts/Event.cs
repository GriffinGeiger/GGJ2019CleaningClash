using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public enum Events { Lava, Puppy, Darkness };

    public Events current;

    public Event(Events type)
    {
        current = type;
    }

    public void execute()
    {
        switch (current)
        {
            case Events.Lava:
                FloorIsLava();
                break;
            case Events.Puppy:
                WhoLetTheDogOut();
                break;
            case Events.Darkness:
                LightsOut();
                break;
            default:
                FloorIsLava();
                break;
        }
    }

    void FloorIsLava()
    {
        LavaController con = new LavaController();
    }

    void WhoLetTheDogOut()
    {
        Dog puppy = new Dog();
    }

    void LightsOut()
    {
        //Remove all light except for from window
    }
}
