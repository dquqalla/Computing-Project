using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.XR.WSA;
using Cinemachine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

// Created using Microsoft's documentation (Link: https://docs.microsoft.com/en-us/windows/mixed-reality/voice-input-in-unity) and a YouTube guide (Link: https://youtu.be/Xau3hFEcn0U).
public class SpeechManager : MonoBehaviour
{
    // "starName" will be used to store the current name of the star. 
    private string starName;
    // Current star will be added to triggeredStar. 
    private GameObject triggeredStar;

    // Reference to AutoSave script. This will be active whenever a user completes a star. 
    public Autosave autoSave;
    // Reference to player. 
    public GameObject player;

    // This is where completed stars will be stored. When the user saves their game, this will be saved too. This is so that whenever the game is loaded next, this list will be iterated through and already completed stars will be removed from the level.
    public static List<string> stars = new List<string>();

    // Objects which will be used for the speech input
    public KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywordActions = new Dictionary<string, System.Action>();

    // Function that enables the speech recognition
    public void EnableSpeech (String currentStarName, GameObject star)
    {
        starName = currentStarName;
        triggeredStar = star;
        // Add the name of current star object with the corresponding function "ObjectCalled" 
        keywordActions.Add(starName, ObjectCalled);
        Debug.Log("The name of this star is ..." + starName);
        // Assign the keywordRecognizer variable and set the confidence level to "low". A low confidence level means the speech does not have to be so accurate. This is very useful considering we will be dealing with aphasia patients. 
        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // Function to disable speech
    public void DisableSpeech ()
    {
        // If keywordRecognizer is not null...
        if (keywordRecognizer != null)
        {
            // Stop, dispose, and clear the keywordRecognizer.
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
            keywordActions.Clear();
        } 
        // Else, return nothing...
        else
        {
            return; 
        }
    }

    // When a phrase is recognised invoke it's respective action which will be to call the function ObjectCalled(). 
    public void OnPhraseRecognized (PhraseRecognizedEventArgs args)
    {
        keywordActions[args.text].Invoke();
    }

    // Function called when object is correctly called by the user.
    public void ObjectCalled ()
    {
        // Hide GameObject as it has been completed by user.
        triggeredStar.SetActive(false);
        stars.Add(triggeredStar.name);

        // End dialogue, play star collect sound.
        FindObjectOfType<DialogueManager>().EndDialogue();
        FindObjectOfType<AudioManager>().Play("StarCollect");

        // Call "UpdateScore" function of scoreSystem.
        FindObjectOfType<ScoreSystem>().UpdateScore();

        // Call "DisableSpeech" function.
        DisableSpeech();

        // Autosave game.
        FindObjectOfType<Player>().SavePlayer();
        autoSave.ActivateIcon();

        // Enable mouse look.
        player.GetComponentInChildren<CinemachineFreeLook>().enabled = true;
    }

}
