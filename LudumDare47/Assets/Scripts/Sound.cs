using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Create New Sound")]
public class Sound : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;
}
