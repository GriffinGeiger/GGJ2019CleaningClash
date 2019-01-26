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
    }

}
