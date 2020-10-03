using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] Sound[] sounds;

    public void PlaySound(string name)
    {
        PlaySound(name, false);
    }

    public void PlaySound(string name, bool loop)
    {
        foreach(Sound s in sounds)
        {
            if(s.soundName.ToLower() == name.ToLower())
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.spatialBlend = 1f;
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch + (Random.value - 0.5f) * s.pitchRandomness;
                source.loop = loop;
                source.Play();
                return;
            }
        }
        Debug.LogError($"Sound {name} does not exist", this);
    }
    public void SpawnSource2D(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.soundName.ToLower() == name.ToLower())
            {
                GameObject spawned = new GameObject();
                spawned.name = $"Spawned Audio Source Playing {name}";
                AudioSource source = spawned.AddComponent<AudioSource>();
                source.spatialBlend = 0f;
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch + (Random.value - 0.5f) * s.pitchRandomness;
                source.loop = false;
                source.Play();
                Destroy(spawned, s.clip.length + 1f);
                return;
            }
        }
        Debug.LogError($"Sound {name} does not exist", this);
    }
}
