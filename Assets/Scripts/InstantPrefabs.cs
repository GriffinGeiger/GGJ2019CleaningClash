using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class InstantPrefabs : MonoBehaviour
{

    public static string prefabPath = "Assets/Prefabs";

    //Throwable items
    public static string commonThrowablePath = prefabPath + "/CommonThrowablePrefabs";
    public static string legosPath = prefabPath + "/LegosPrefabs";
    public static string socksPath = prefabPath + "/SocksPrefabs";
    public static string draggablePath = prefabPath + "/Draggables";

    //Players
    public static string player1Path = prefabPath + "/Characters/Character_Player1.prefab";
    public static string player2Path = prefabPath + "/Characters/Test_Character_Player2 Variant.prefab";

    //Placable items
    private static string bedPath_P1 = draggablePath + "/Bed_Draggable_P1.prefab";
    private static string bedPath_P2 = draggablePath + "/Bed_Draggable_P2.prefab";
    public static string deskPath = draggablePath + "/Desk_Prefab.prefab";
    public static string fanPath = draggablePath + "/Fan_Prefab.prefab";
    public static string dresserPath = draggablePath + "/Dresser_Prefab.prefab";

    //Powerups
    public static string sugarRushPath = prefabPath + "/SugarRushPrefabs";
    public static string infinityGauntletPath = prefabPath + "/InfinityGauntletPrefabs";
    public static string hulkHandsPath = prefabPath + "/HulkHandsPrefabs";
    public static string batteryPath = prefabPath + "/BatteryPrefabs";

    //Events
    public static string lavaPath = prefabPath + "/LavaPrefabs";
    public static string dogPath = prefabPath + "/DogPrefabs";
    public static string darkPath = prefabPath + "/DarkPrefabs";

    //********************  Base Instantiate class **********************

    public static GameObject InstantiatePrefab(string path, Vector3 position)
    {
        Debug.Log("Spawning at path: " + path);
        return GameObject.Instantiate(
            AssetDatabase.LoadAssetAtPath<GameObject>(path), position, Quaternion.identity);
    }

    //********************  Throwable items **********************

    public static GameObject SpawnRandomCommonThrowable(Vector3 position)
    {
        try
        {
            System.Random rng = new System.Random();
            int randomNumber = rng.Next(0, 10);
            return InstantiatePrefab(commonThrowablePath + "/Common_Throwable ("
                + randomNumber + ") Variant.prefab", position);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public static GameObject SpawnLegos(Vector3 position)
    {
        return InstantiatePrefab(legosPath, position);
    }

    public static GameObject SpawnSocks(Vector3 position)
    {
        return InstantiatePrefab(socksPath, position);
    }

    //********************  Powerups **********************

    public static GameObject SpawnSugarRush(Vector3 position)
    {
        return InstantiatePrefab(sugarRushPath, position);
    }

    public static GameObject SpawnInfinityGauntlet(Vector3 position)
    {
        return InstantiatePrefab(infinityGauntletPath, position);
    }

    public static GameObject SpawnHulkHands(Vector3 position)
    {
        return InstantiatePrefab(hulkHandsPath, position);
    }

    public static GameObject SpawnBattery(Vector3 position)
    {
        return InstantiatePrefab(batteryPath, position);
    }

    //********************  Events **********************

    public static GameObject SpawnLava(Vector3 position)
    {
        return InstantiatePrefab(lavaPath, position);
    }

    public static GameObject SpawnDog(Vector3 position)
    {
        return InstantiatePrefab(dogPath, position);
    }

    public static GameObject SpawnDark(Vector3 position)
    {
        return InstantiatePrefab(darkPath, position);
    }

    //********************  Furniture **********************
    public static GameObject SpawnBed(Vector3 position, PlayerInput.playerTag playerTag) //returns null if not player1 or 2
    {
        if (playerTag == PlayerInput.playerTag.Player1)
        {
            Debug.Log("Spawining Player1 bed");
            return InstantiatePrefab(bedPath_P1, position);
        }
        else
            if (playerTag == PlayerInput.playerTag.Player2)
        {
            Debug.Log("Spawning Player2 bed");
            return InstantiatePrefab(bedPath_P2, position);
        }
        else
            return null;
    }

    public static GameObject SpawnDesk(Vector3 position)
    {
        return InstantiatePrefab(deskPath, position);
    }

    public static GameObject SpawnFan(Vector3 position)
    {
        return InstantiatePrefab(fanPath, position);
    }

    public static GameObject SpawnDresser(Vector3 position)
    {
        return InstantiatePrefab(dresserPath, position);
    }
}
