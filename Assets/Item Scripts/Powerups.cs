using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : Item
{
    public enum Types { SugarRush, InfinityGauntlet, HulkHands, SpawnBattery, GasMask, CellPhone };

    public Types powerType;

    void Activate()
    {
        switch(powerType)
        {
            case Types.SugarRush:
                sugarRush();
                break;
            case Types.InfinityGauntlet:
                infinityGuantlet();
                break;
            case Types.HulkHands:
                hulkHands();
                break;
            case Types.SpawnBattery:
                spawnBattery();
                break;
            case Types.GasMask:
                gasMask();
                break;
            case Types.CellPhone:
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
