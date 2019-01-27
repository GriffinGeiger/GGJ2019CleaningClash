using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager gm = (GameManager)target;

        if(GUILayout.Button("Allow all characters to move"))
        {
            gm.allowCharacterMovement();
        }
        if (GUILayout.Button("Disallow all character movement"))
        {
            gm.disallowCharacterMovement();
        }
        if(GUILayout.Button("Test spawn an item"))
        {
            InstantPrefabs.InstantiatePrefab(InstantPrefabs.commonThrowablePath + "/Common_Throwable.prefab", Vector3.zero);
        }
        if(GUILayout.Button("Spawn random common item"))
        {
            InstantPrefabs.SpawnRandomCommonThrowable(Vector3.zero);
        }
        if (GUILayout.Button("Tally Scores"))
        {
            int winner = gm.TallyScores();
            if (winner == 1)
                Debug.Log("Player 1 wins!");
            else if (winner == 2)
            {
                Debug.Log("Player 2 wins!");
            }
            else if (winner == 0)
            {
                Debug.Log("Everyone is grounded");
            }
        }
    }

}
