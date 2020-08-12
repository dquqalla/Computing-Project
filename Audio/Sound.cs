using UnityEngine.Audio;
using UnityEngine;

// Credit to Brackey's tutorial on YouTube (Link: https://youtu.be/CE9VOZivb3I). This script is responsible for managing the audio in different scenes.
// This script defines a Sound object. It contains values such as the name of the sound clip, the source of the clip, its volume, and whether or not it should loop in-game. These will be edited in the inspector. 
[System.Serializable]
public class Sound
{
    // Contains name of audio file.
    public string name;

    // Contains reference to audio clip.
    public AudioClip clip;

    // Stores volume level of clip.
    [Range(0f, 1f)]
    public float volume;

    // Stores boolean of whether or not to loop audio clip.
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
