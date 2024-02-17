using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        win.maxSize = new Vector2(500, 600);
        win.minSize = new Vector2(500, 600);
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

                GUIStyle yellowBackgroundStyle = new GUIStyle(GUI.skin.button);

                yellowBackgroundStyle.normal.background = MakeBackgroundTexture(10, 10, Color.red);

                if (GUILayout.Button(new GUIContent("Remove"), yellowBackgroundStyle))
                {
                    if (EditorUtility.DisplayDialog("Erorr", "Are you sure you want remove this", "OK", "Cancel"))
                    {

                        _ListSounds.Remove(soud);
                    }
                }

                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(4);
                EditorGUILayout.EndHorizontal();
                i++;
            }
            EditorGUILayout.Space(10);

            EditorGUILayout.EndVertical();
        }
        if (GUILayout.Button("Add"))
        {
            Sound sound = new Sound();
            _ListSounds.Add(sound);
        }
        if (GUILayout.Button("Save", GUILayout.Height(70)))
        {
            Debug.Log("Save");
            soundObject.Sounds = _ListSounds;
        }
        Repaint();
    }

    private Texture2D MakeBackgroundTexture(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D backgroundTexture = new Texture2D(width, height);

        backgroundTexture.SetPixels(pixels);
        backgroundTexture.Apply();

        return backgroundTexture;
    }

    void OnDestroy()
    {
        Debug.Log("Save");
        soundObject.Sounds = _ListSounds;
    }


}
