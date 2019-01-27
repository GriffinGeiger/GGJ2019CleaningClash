using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayer
{
    enum SetupState { Bed, Desk, Fan, Dresser, Done };
    SetupState setupState;
    SetupState nextState;
    bool readyForNextFurniture = true;
    DraggableObject currentDraggable;
    private PlayerInput.playerTag playerTag;

    public SetupPlayer(PlayerInput.playerTag playerTag)
    {
        this.playerTag = playerTag;
    }

    //Called every update during setup
    public bool PlayerSetup(Vector3 spawnLocationForItems)
    {
        if (readyForNextFurniture) //spawn next furniture and set the nextstate
        {
            switch (setupState)
            {
                case SetupState.Bed:
                    currentDraggable = InstantPrefabs.SpawnBed(spawnLocationForItems,playerTag).GetComponent<DraggableObject>();
                    nextState = SetupState.Desk;
                    break;
                case SetupState.Desk:
                    currentDraggable = InstantPrefabs.SpawnDesk(spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Fan;
                    break;
                case SetupState.Fan:
                    currentDraggable = InstantPrefabs.SpawnFan(spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Dresser;
                    break;
                case SetupState.Dresser:
                    currentDraggable = InstantPrefabs.SpawnDresser(spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Done;
                    break;
                case SetupState.Done:
                    return true;
            }
            if(currentDraggable!= null)
                currentDraggable.ConnectToPlayerInput();
            readyForNextFurniture = false;
        }

        if (currentDraggable.placed) //object has been placed, go to next one
        {
            setupState = nextState;
            readyForNextFurniture = true;
        }

        return false;
    }
}
