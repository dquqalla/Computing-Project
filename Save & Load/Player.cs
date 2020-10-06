using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// Credit to Brackey's tutorial for providing guidance on how to create a save system (Link: https://youtu.be/XOjd_qU2Ido). 
public class Player : MonoBehaviour
{
    // Contains reference to relevant data of the current player i.e. their current level, saved state, current score, and list of completed stars
    public int level;
    public int currentScore;
    public List<string> stars = new List<string>();

    // Save function
    public void SavePlayer ()
    {
        // Level will store the current level the user is in.
        level = SceneManager.GetActiveScene().buildIndex;

        // Current score will be equalled to the value stored by ScoreSystem.
        currentScore = ScoreSystem.currentScore;

        // Stars will be equal to the list array stored by the SpeechManager object.
        stars = SpeechManager.stars;

        // Save data of player character, i.e. their position. 
        SaveSystem.SavePlayer(this);
    }
}
