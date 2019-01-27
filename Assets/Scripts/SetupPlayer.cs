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
    //Called every update during setup
    public bool PlayerSetup(Vector3 spawnLocationForItems)
    {
        if (readyForNextFurniture) //spawn next furniture and set the nextstate
        {
            switch (setupState)
            {
                case SetupState.Bed:
                    currentDraggable = InstantPrefabs.InstantiatePrefab(InstantPrefabs.bedPath, spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Desk;
                    break;
                case SetupState.Desk:
                    currentDraggable = InstantPrefabs.InstantiatePrefab(InstantPrefabs.deskPath, spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Fan;
                    break;
                case SetupState.Fan:
                    currentDraggable = InstantPrefabs.InstantiatePrefab(InstantPrefabs.fanPath, spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Dresser;
                    break;
                case SetupState.Dresser:
                    currentDraggable = InstantPrefabs.InstantiatePrefab(InstantPrefabs.bedPath, spawnLocationForItems).GetComponent<DraggableObject>();
                    nextState = SetupState.Done;
                    break;
                case SetupState.Done:
                    return true;
            }
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
