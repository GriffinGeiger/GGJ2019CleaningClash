using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    Random rand;
    public Text timer = null;

    //                    lava,  dog, lights
    float[] EventProbs = { 50f,  30f,  10f };

    //                  common, legos, socks
    static float[] ItemProbs = { 80f,   10f,   10f };

    //                      sugar, gauntlet, hands, battery, mask, phone
    float[] PowerupProbs = { 10f,     10f,    10f,    2f,     10f,   1f };

    

    //An algorithm for using RNG to select an option from a weighted number set
    static int Choose (float[] probs)
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
    Event.Events chooseEvent()
    {
        int choice = Choose(EventProbs);
        switch (choice)
        {
            case 0:
                return Event.Events.Lava;
            case 1:
                return Event.Events.Puppy;
            case 2:
                return Event.Events.Darkness;
            default:
                return Event.Events.Darkness;
        }
    }

    //Method for choosing a random item to be created
    Item chooseItem()
    {
        int choice = Choose(ItemProbs);
        switch (choice)
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
        int choice = Choose(PowerupProbs);
        switch (choice)
        {
            case 0:
                return Powerups.PowerUp.SugarRush;
            case 1:
                return Powerups.PowerUp.InfinityGauntlet;
            case 2:
                return Powerups.PowerUp.HulkHands;
            case 3:
                return Powerups.PowerUp.SpawnBattery;
            case 4:
                return Powerups.PowerUp.GasMask;
            case 5:
                return Powerups.PowerUp.CellPhone;
            default:
                return Powerups.PowerUp.SpawnBattery;
        }
    }

    IEnumerator EventStart(Event e)
    {
        string eventName = "";
        Event.Events current = e.current;
        switch (current)
        {
            case Event.Events.Lava:
                eventName = "Floor is lava";
                break;
            case Event.Events.Puppy:
                eventName = "Who let the dogs out";
                break;
            case Event.Events.Darkness:
                eventName = "Lights out";
                break;
        }
        string time = " is starting in 5 seconds";
        timer.text = eventName + time;
        yield return new WaitForSeconds(5.0f);
    }

    static private Vector2 maxPoint= new Vector2(8f,1f);
    static private Vector2 minPoint = new Vector2(-8f, -4.3f);
    static private Vector2 midPoint = new Vector2(0f, -1.7f);
    //Randomly spawns items on each team's side
    static public void SpawnItems(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                float min = j == 0 ? minPoint.x : midPoint.x;
                float max = j == 0 ? midPoint.x : maxPoint.x;
               
                int num = Choose(ItemProbs);

                Vector2 loc = new Vector2(Random.value * max, Random.value * 5.3f - 4.3f);
                InstantPrefabs.SpawnRandomCommonThrowable(loc) ;
                /*while (true)
                {
                    
                    
                    if (Physics2D.OverlapCircle(loc, .01f) == null){
                        //if (num == 0)
                            InstantPrefabs.SpawnRandomCommonThrowable(loc);  
                        //else if (num == 1) 
                             //InstantPrefabs.SpawnLegos(loc);                       
                        //else
                             //InstantPrefabs.SpawnSocks(loc);
                        break;
                    }
                }*/

            }
        }
    }

    //Randomly spawns a powerup on each team's side
    void SpawnPowerup()
    {
        for (int j = 0; j < 2; j++)
        {
            int min = j == 0 ? 0 : Screen.width / 2;
            int max = j == 0 ? Screen.width / 2 : Screen.width;

            int num = Choose(PowerupProbs);
            while (true)
            {
                Vector2 loc = new Vector2(Random.Range(min, max), Random.Range(0, Screen.height));

                if (Physics2D.OverlapCircle(loc, .5f) == null)
                {
                    if (num == 0){
                        InstantPrefabs.SpawnSugarRush(loc);
                        break;
                    }
                    else if (num == 1){
                        InstantPrefabs.SpawnInfinityGauntlet(loc);
                        break;
                    }
                    else  if (num == 2){
                        InstantPrefabs.SpawnHulkHands(loc);
                        break;
                    }
                    else if (num == 3){
                        InstantPrefabs.SpawnBattery(loc);
                        break;
                    }
                    /**
                    * 
                    *
                    else if (num == 0){
                        InstantPrefabs.SpawnGasMask(loc);
                        break;
                    }
                    else {
                        InstantPrefabs.SpawnCellPhone(loc);
                        break;
                    }
                    **/

                }
            }

        }
        
    }

    //Randomly spawns a powerup on each team's side
    void ChooseAndStartEvent()
    {
        Event e = new Event(chooseEvent());
        StartCoroutine("EventStart", e);
    }
}
