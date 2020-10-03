using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    public void PlaySound(string name)
    {
        foreach(Sound s in sounds)
        {
            if(s.soundName.ToLower() == name.ToLower())
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.spatialBlend = 1f;
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.Play();
                return;
            }
        }
        Debug.LogError($"Sound {name} does not exist", this);
    }
}
