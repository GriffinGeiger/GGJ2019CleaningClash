using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : Item
{
    public enum Types { SugarRush, InfinityGauntlet, HulkHands };

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
        }
    }

    private void sugarRush()
    {
        
    }

    private void infinityGuantlet()
    {

    }

    private void hulkHands()
    {

    }

    public override void Pickup() 
    {
        Activate();
    }
}
