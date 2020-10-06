using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

// This script was created with the help of Brackey's tutorial (Link: https://youtu.be/_nRzoTzeyxU). 
// Brackey's script has been amended to implement the SpeechManager script and other functions too (i.e. playing audio clips, enabling/disabling camera movement, etc). 
public class DialogueManager : MonoBehaviour
{
    // Declare variables which will be used to present dialogue to the UI for the user. 
    // Queue "sentences" will contain all the sentences for the star triggered by the user. 
    private Queue<string> sentences;

    // TextMeshProUGUI variables which will be used to store the name of the object and it's respective dialogue. 
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    // Float variable declared which can adjust the rate at which dialogue is shown. 
    public float typingSpeed;

    // GameObject variable "contineButton" refers to the contine button shown in the UI to progress to the next dialogue.
    public GameObject continueButton;
    public GameObject playNameButton;

    // Declare audioManager variable. This will be called to play audio dialogue which will be said to the user. 
    // The use of audio in the dialogue will allow the user to understand what is being said. 
    public AudioManager audioManager;
    // "audioIdx" will be used to keep track of which audio clip to play next (dependent on star). 
    public int audioIdx;

    // Animator will be used to add some animation effect to the dialogue as it will swipe up from the bottom of the canvas. 
    public Animator animator;

    // Declaration of GameObjects. triggeredStar will be the star triggered by the user when they collide with it.
    // "player" refers to character in-scene. 
    private GameObject triggeredStar;
    public GameObject player;

    // Start is called before the first frame update
    void Start ()
    {
        // Create new queue which will store sentences. 
        sentences = new Queue<string>();
        audioIdx = -1;
    }

    // Function that will start the on-screen dialogue. 
    public void StartDialogue (Dialogue dialogue, GameObject star)
    {
        // Set animator boolean to true which will bring up the dialogue. 
        animator.SetBool("isOpen", true);

        // Assign the name of the star to nameText.
        nameText.text = dialogue.name;
        // Assign triggerdStar the GameObject star. 
        triggeredStar = star;
        // Clear queue if anything exists. 
        sentences.Clear();

        // For each sentence in dialogue.sentences string array...
        foreach (string sentence in dialogue.sentences)
        {
            // Enqueue the sentence into sentences queue. 
            sentences.Enqueue(sentence);
        }

        // Call DisplayNextSentence(). 
        DisplayNextSentence();
    }

    // Function which will display first/next sentence to UI. 
    public void DisplayNextSentence ()
    {
        // Increment audioIdx to play next dialogue audio.
        audioIdx++;

        // Declare a new string "sentence" which will be assigned the value of the first sentence in the queue. 
        string sentence = sentences.Dequeue();

        // Call PlayAudio() which will read out the sentence to the user. 
        PlayAudio();

        // Stop any current coroutine. 
        StopAllCoroutines();

        // Star a new coroutine "TypeSentence(sentence)" passing the current sentence as the parameter. 
        StartCoroutine(TypeSentence(sentence));

        // If it is the last sentence of the dialogue, this is when we will call the speech function to take the user's microphone input.
        if (sentences.Count == 0)
        {
            // Set continue button to false as it is not required. Set play name button to true. 
            continueButton.SetActive(false);
            playNameButton.SetActive(true);
            
            /* Enable speech recognition after 1 second. The reason for this is that if the user is using speakers for audio, 
               the speech recognition may pick up the dialogue audio when it (sometimes) says the object's name. */
            Invoke("EnableSpeech", 1.0f); 
        }

    }

    // Function used for typing effect on screen. 
    IEnumerator TypeSentence (string sentence)
    {
        // This boolean will keep track whether or not we need to keep the continue button active. 
        bool activateBtn; 

        // If sentences have finished, deactivate continue button.
        if (sentences.Count == 0)
        {
            activateBtn = false;
        } 
        // Else, keep it active.
        else
        {
            activateBtn = true;
        }

        // Initially, set the continue button to inactive. 
        continueButton.SetActive(false);

        // The dialogue text will be blanked out. This is in order to prevent randomly generated sentences appearing on-screen.
        dialogueText.text = "";

        // For each letter in the current string "sentence", load each letter one-by-one. This is to get that typing effect on-screen.
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Determine which sentence we are presenting and whether or not we need to activate the continue button.
        continueButton.SetActive(activateBtn);
    }

    public void EnableSpeech()
    {
        FindObjectOfType<SpeechManager>().EnableSpeech(nameText.text, triggeredStar);
    }

    // Function to play audio of the current dialogue on-screen.
    public void PlayAudio ()
    {
        // If it is the first sentence, play the first audio clip.
        if (audioIdx == 0)
        {
            audioManager.Play(nameText.text + audioIdx);

        }
        // If a user is to skip to the next dialogue and the audio hasn't finished stop the previous audio clip and play the next one. This is so audio clips do not play simultaneously.
        else if (audioIdx > 0)
        {
            audioManager.Stop(nameText.text + (audioIdx - 1));
            audioManager.Play(nameText.text + audioIdx);
        }
    }

    // Function which will set the animator to false as the user has successfully completed the star. 
    public void EndDialogue ()
    {
        audioManager.Stop(nameText.text + audioIdx);
        audioIdx = -1;
        animator.SetBool("isOpen", false);
        playNameButton.SetActive(false);
    }

    public void PlayName ()
    {
        FindObjectOfType<SpeechManager>().keywordRecognizer.Stop();
        audioManager.Play(nameText.text + "Name");
        FindObjectOfType<SpeechManager>().keywordRecognizer.Start();
    }
    
    // Function which will re-enable camera after the user has successfully completed the star.
    public void EnableCamera ()
    {
        player.GetComponentInChildren<CinemachineFreeLook>().enabled = true;
    }

    // Function which will disable the camera as the user tries to complete the star. 
    public void DisableCamera ()
    {
        player.GetComponentInChildren<CinemachineFreeLook>().enabled = false;
    }
}
