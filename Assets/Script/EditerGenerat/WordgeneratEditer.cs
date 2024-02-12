using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine.WSA;
using System;
using UnityEditor.Rendering;

public class WordgeneratEditer : EditorWindow
{
    [MenuItem("Window/Generat World")]
    public static void ShowWindow()
    {
        GetWindow<WordgeneratEditer>("Generat Word");
    }
    private void OnGUI()
    {

        if (GUILayout.Button("Generat Word"))
        {

            spwinPlatform ff = FindAnyObjectByType<spwinPlatform>();
            ff.Spwin();
            Destroy(ff);

            // for (int i = 0; i < 10; i++)
            // {
            //     Debug.Log("Generat Word");
            // }
        }
    }


}
