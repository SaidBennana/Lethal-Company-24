using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SoundEditor : EditorWindow
{
    static SoundEditor win;

    #region variables
    static SoundObject soundObject;
    Vector2 scrolle;
    static List<Sound> _ListSounds = new List<Sound>();
    #endregion 

    public static void openWindow()
    {
        win = GetWindow<SoundEditor>(typeof(SoundEditor));
        win.maxSize=new Vector2(500,600);
        win.minSize=new Vector2(500,600);
        win.Show();

        //soundObject = (SoundObject)AssetDatabase.LoadAssetAtPath("Assets/Script/Sound Manager/SoundData.asset", typeof(SoundObject));


    }

    private void OnGUI()
    {
        if (soundObject)
        {

            _ListSounds = soundObject.Sounds;
        }
        else
        {
            if (File.Exists("Assets/Script/Sound Manager/SoundData.asset"))
            {
                Debug.Log(
                    "done"
                );
            }
            soundObject = (SoundObject)AssetDatabase.LoadAssetAtPath("Assets/Script/Sound Manager/SoundData.asset", typeof(SoundObject));

        }
        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrolle, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true)))
        {
            if (!soundObject)
            {
                EditorGUILayout.LabelField("File SoundDtat Not Exsis");
                if (GUILayout.Button("Create Sound Data"))
                {
                    SoundObject SoundObjectCreate = new SoundObject();
                    AssetDatabase.CreateAsset(SoundObjectCreate, "Assets/Script/Sound Manager/SoundData.asset");

                }
                return;
            }
            scrolle = scrollView.scrollPosition;
            EditorGUILayout.BeginVertical();
            int i = 0;
            foreach (Sound soud in _ListSounds.ToList())
            {
                EditorGUILayout.Space(20);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(4);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Audio Index " + i);
                EditorGUILayout.Space(10);

                soud.nameClip = EditorGUILayout.TextField("Name Clip", soud.nameClip);
                EditorGUILayout.Space(5);
                soud.clip = (AudioClip)EditorGUILayout.ObjectField("Audio Clap", soud.clip, typeof(AudioClip));
                EditorGUILayout.Space(5);
                soud.volume = EditorGUILayout.Slider("volume", soud.volume, 0, 1);
                EditorGUILayout.Space(4);
                if (GUILayout.Button("Remove"))
                {
                    _ListSounds.Remove(soud);
                }

                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(4);
                EditorGUILayout.EndHorizontal();
                i++;
            }
            EditorGUILayout.Space(10);

            if (GUILayout.Button("Add"))
            {
                Sound sound = new Sound();
                _ListSounds.Add(sound);
            }
            EditorGUILayout.EndVertical();
        }
        Repaint();
    }


}
