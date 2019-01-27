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

    //An algorithm for using RNG to select an option from a weighted number set
    float Choose (float[] probs)
    {
        float total = 0;
        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i=0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    //Method for choosing a random event to be created
    Events chooseEvent()
    {
        float[] probs = { 50f, 30f, 10f };
        float choice = Choose(probs);
        switch ((int)choice)
        {
            case 0:
                return Events.Lava;
            case 1:
                return Events.Puppy;
            case 2:
                return Events.Darkness;
            default:
                return Events.Darkness;
        }
    }

    //Method for choosing a random item to be created
    Item chooseItem()
    {
        //              common, lego, socks
        float[] probs = { 80f, 10f, 10f };
        float choice = Choose(probs);
        switch ((int)choice)
        {
            case 0:
                return new Item();
            case 1:
                return new Legos();
            case 2:
                return new Socks();
            default:
                return new Item();
        }


    }

    //Method for choosing a random powerup to be created
    Powerups.PowerUp choosePowerUp()
    {
        float[] probs = { 10f, 10f, 10f, 10f, 10f, 1f };
        float choice = Choose(probs);
        switch ((int)choice)
        {
            case 0:
                return Powerups.PowerUp.CellPhone;
            case 1:
                return Powerups.PowerUp.GasMask;
            case 2:
                return Powerups.PowerUp.HulkHands;
            case 3:
                return Powerups.PowerUp.InfinityGauntlet;
            case 4:
                return Powerups.PowerUp.SugarRush;
            case 5:
                return Powerups.PowerUp.SpawnBattery;
            default:
                return Powerups.PowerUp.SpawnBattery; ;
        }
    }


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

    //Randomly spawns items on each team's side
    void SpawnItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                int min = j == 0 ? 0 : Screen.width / 2;
                int max = j == 0 ? Screen.width / 2 : Screen.width;
               
                //              common, lego, socks
                float[] probs = { 80f, 10f, 10f };
                
                int num = (int)Choose(probs);
                while (true)
                {
                    Vector2 loc = new Vector2(Random.Range(min, max), Random.Range(0, Screen.height));
                    
                    if (Physics2D.OverlapCircle(loc, .5f) == null){
                        if (num == 0)
                        {
                            InstantPrefabs.SpawnRandomCommonThrowable(loc);
                        }
                        /**
                         * else if (num == 1) {
                         *     InstantPrefabs.SpawnLegos(loc);
                         * }
                         * 
                         * else{
                         *      InstantPrefabs.SpawnSocks(loc);
                         * }
                         * 
                         **/
                        break;
                    }
                }

            }
        }
    }
}
