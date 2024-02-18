using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

static class ToolbarStyles
{
    public static readonly GUIStyle commandButtonStyle;

    static ToolbarStyles()
    {
        commandButtonStyle = new GUIStyle("Command")
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageAbove,
            fontStyle = FontStyle.Normal
        };
    }
}


[InitializeOnLoad]
public class DeleteSave
{

    static DeleteSave()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    }

    static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        if (GUILayout.Button(new GUIContent("DEL", "Delete Unique Save"), ToolbarStyles.commandButtonStyle))
        {
          
            Debug.Log("<color=red>All Player Prefs Data Cleared! :) </color>");

            PlayerPrefs.DeleteAll();
        }
    }
}