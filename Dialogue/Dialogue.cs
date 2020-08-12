using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was created with the help of Brackey's tutorial (Link: https://youtu.be/_nRzoTzeyxU). 
// This script contains the values used for the "DialogueTrigger" script. The string "name" will be passed into the SpeechManager.  
[System.Serializable]
public class Dialogue
{
    // The name of the current star will be stored in this variable. 
    public string name;
    
    // Array of sentences which will be converted to dialogue on-screen.
    [TextArea(3, 10)]
    public string[] sentences;
}
