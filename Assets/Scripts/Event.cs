using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    Random rand;
    public Text timer = null;

    public enum Events { Lava, Puppy, Darkness };

    private Events current;


    void FloorIsLava()
    {
        current = Events.Lava;
        StartCoroutine("EventStart");
        timer = null;
    }

    void WhoLetTheDogOut()
    {
        current = Events.Puppy;
        StartCoroutine("EventStart");
        timer = null;
        Dog puppy = new Dog();
    }

    void LightsOut()
    {
        current = Events.Darkness;
        StartCoroutine("EventStart");
        timer = null;
        //Remove all light except for from window
    }

    IEnumerator EventStart()
    {
        string eventName = "";
        switch (current)
        {
            case Events.Lava:
                eventName = "Floor is lava";
                break;
            case Events.Puppy:
                eventName = "Who let the dogs out";
                break;
            case Events.Darkness:
                eventName = "Lights out";
                break;
        }
        string time = " is starting in 5 seconds";
        timer.text = eventName + time;
        yield return new WaitForSeconds(5.0f);
    }

    void SpawnItems()
    {
        
        //for each side of the room
            //Randomly pick a spot to place an item
            //If it would overlap with something, pick a different one
            //Randomly select one of the items to spawn
    }
}
