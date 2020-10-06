using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

// This script implements the score system of the game. It contains functions to update the score as the user progresses throughout the level.
// Created using simple increment method. Learnt from use of other languages such as Java.
public class ScoreSystem : MonoBehaviour
{
    // Score objects
    public TextMeshProUGUI scoreText;
    public static int currentScore;
    public int levelDone;

    // References to GameObjects. "levelComplete" - text object which is activated and shown to the user once all stars have been completed. "stars" - game object containing all the stars within a level. 
    public GameObject levelComplete;
    public GameObject stars;
   
    // Store GUI GameObjects
    public GameObject waypoints;
    public GameObject scoreUI;

    public Animator characterAnim;

    // Function called at first frame. 
    void Start()
    {
        levelDone = 0;

        // If a saved file exists.
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
        {
            // Load "data" object with saved file data.
            PlayerData data = SaveSystem.LoadPlayer();

            // If the user is in another level not matching the saved file level, load the default value for the value. 
            if (data.level != SceneManager.GetActiveScene().buildIndex)
            {
                currentScore = 0;
                scoreText.text = "STARS: 0/" + stars.transform.childCount;
            }
            // Else update the score to reflect the saved score when the user last played the game. 
            else
            {
                currentScore = data.currentScore;
                scoreText.text = "STARS: " + currentScore + "/" + stars.transform.childCount;

                if(currentScore == stars.transform.childCount)
                {
                    LevelCompleted();
                }
            }       
        }
        // Else...
        else
        {
            currentScore = 0;
            // Load default value.
            scoreText.text = "STARS: 0/" + stars.transform.childCount;
        }
    }

    // Function to update the score when a user completes a star.
    public void UpdateScore()
    {
        // Increment score by one. 
        currentScore += 1;
        // Update score to reflect new score. 
        scoreText.text = "STARS: " + currentScore + "/" + stars.transform.childCount;

        // If the current score is equalled to the total amount of stars within the level...
        if (currentScore == stars.transform.childCount)
        {
            LevelCompleted();
        }
    }

    // Function to call when all stars have been collected. 
    public void LevelCompleted()
    {
        // Set levelComplete text object to true, set all other GUIs to false.
        levelComplete.SetActive(true);
        waypoints.SetActive(false);
        scoreUI.SetActive(false);

        // Trigger the dancing animation and start the countdown to load the next level.
        characterAnim.SetTrigger("Dancing");
        Invoke("NextLevel", 4.0f);
        
        // Set int of "levelDone" to 1. This is used in-case the user pauses the game and resumes it. The pause script will check if this is 1 and set the "levelComplete" text object to true again.
        levelDone = 1; 
    }

    void NextLevel()
    {
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
