using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Sound")]
public class SoundObject : ScriptableObject
{
    public List<Sound> Sounds = new List<Sound>();

}

[Serializable]
public class Sound
{
    public string nameClip;
    [HideInInspector] public AudioSource source;
    [Space(10)]
    public AudioClip clip;
    [Header("Configer")]
    [Range(0, 1)] public float volume = 1f;
}
