﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

public class InstantPrefabs : MonoBehaviour
{

    public static string prefabPath = "Assets/Prefabs";
    public static string commonThrowablePath = prefabPath + "/CommonThrowablePrefabs";


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

    public static GameObject InstantiatePrefab(string path, Vector3 position)
    {
        Debug.Log("Spawning at path: " + path);
        return GameObject.Instantiate(
            AssetDatabase.LoadAssetAtPath<GameObject>(path), position, Quaternion.identity);
    }


}