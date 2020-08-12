using UnityEngine.Audio;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

// Credit to Brackey's tutorial on YouTube (Link: https://youtu.be/CE9VOZivb3I). This script is responsible for managing the audio in different scenes.
// This script contains the functions used to define "sounds" objects and their respective values.
public class AudioManager : MonoBehaviour
{
    // Refer to sounds array (see Sound.cs file) and AudioMixerGroup objects.
    public Sound[] sounds;
    public AudioMixerGroup master;

    // Called when game object containing script is instantiated. 
    void Awake()
    {
        // For each sound s in sounds array, create a component in-game and assign it their respective values. 
        foreach (Sound s in sounds)
        {
            // Define values for each sound clip, i.e. source, clip, volume, loop/not loop, mixer group. 
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = master;
        }
    }

    // Function called at first frame. 
    void Start()
    {
        // If a saved file exists, only play the background music. 
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
            Play("BackgroundMusic");
        // Else, play the "WelcomeAudio" which is only played when the user starts a new game.
        else
        {
            Play("WelcomeAudio");
            Play("BackgroundMusic");
        }  
    }

    // Function called when an audio is played. Parameter contains the string "name" which refers to the name of the audio component to be played.
    public void Play(string name)
    {
        // Search in array to find audio with matching name passed into function. 
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // If audio does not exist, return nothing.
        if (s == null)
            return;

        // Otherwise, play the audio. 
        s.source.Play();
    }

    // Function called to stop current audio being played. Parameter contains the string "name" which refers to the name of the audio component to be played.
    public void Stop(string name)
    {
        // Search in array to find audio with matching name passed into function. 
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // If audio does not exist, return nothing.
        if (s == null)
            return;

        // Otherwise, play the audio. 
        s.source.Stop();
    }

}
