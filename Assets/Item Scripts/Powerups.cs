using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : Item
{
    public enum PowerUp { SugarRush, InfinityGauntlet, HulkHands, SpawnBattery, GasMask, CellPhone };

    public PowerUp powerType;

    void Activate()
    {
        switch(powerType)
        {
            case PowerUp.SugarRush:
                sugarRush();
                break;
            case PowerUp.InfinityGauntlet:
                infinityGuantlet();
                break;
            case PowerUp.HulkHands:
                hulkHands();
                break;
            case PowerUp.SpawnBattery:
                spawnBattery();
                break;
            case PowerUp.GasMask:
                gasMask();
                break;
            case PowerUp.CellPhone:
                cellPhone();
                break;
        }
    }

    private void sugarRush()
    {
        //Player movement speed is increased for a short period of time and different music plays
    }

    private void infinityGuantlet()
    {
        //Half of items are deleted and the remainder are split evenly accross the rooms
    }

    private void hulkHands()
    {
        //Increases player's throw strength
    }

    private void spawnBattery()
    {
        //Spawn the player a new battery for their fan
    }

    private void gasMask()
    {
        //Gives the player a gas mask, granting immunity to stink bombs
    }

    private void cellPhone()
    {
        //Distracts (stuns) the player that picks it up
    }

    public override Item Pickup() 
    {
        Activate();
        return null; //this one doesn't return anything because you can't hold powerups
    }
}
