using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

// This script is responsible for what to do when the user collides with a star in-level. It contains the relevant functions that will be called when the user enters/exits the colldier. 
public class DialogueTrigger : MonoBehaviour
{
    // Call to Dialogue script
    public Dialogue dialogue;

    // Boolean values
    private bool isTriggered = false;
    
    // Function called when user enters collider.
    public void TriggerDialogue ()
    {
        // Call functions of DialogueManager script StartDialogue() and DisableCamera(). 
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, gameObject);
        FindObjectOfType<DialogueManager>().DisableCamera();

        // Show cursor
        Cursor.visible = true;
    }

    // Function called when user exits collider.
    public void ExitDialogue ()
    {
        // Call functions of SpeechManager and DialogueManager script. Scripts called are DisableSpeech(), EndDialogue() and EnableCamera(). Essentially, when a user exits a star collider microphone input is disabled, dialogue is removed from screen, and the camera is reenabled. 
        FindObjectOfType<SpeechManager>().DisableSpeech();
        FindObjectOfType<DialogueManager>().EndDialogue();
        FindObjectOfType<DialogueManager>().EnableCamera();

        // Do not show cursor
        Cursor.visible = false;
    }

    // If player collides with a star...
    void OnTriggerEnter (Collider other)
    {
        // If isTriggered is false 
        if (isTriggered == false)
        {
            // Set isTriggered to true 
            isTriggered = true;

            // Enable dialogue on-screen 
            TriggerDialogue();
        }
    }

    // When player exits the star collider...
    void OnTriggerExit (Collider other)
    {
        // If isTriggered is true
        if (isTriggered == true)
        {
            // Set isTriggered to false
            isTriggered = false;
            // Disable dialogue on-screen + speech
            ExitDialogue();
            
        }
    }
}
