using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Credit to Brackey's tutorial on YouTube (Link: https://youtu.be/CE9VOZivb3I). This script is responsible for the transitions between screens. 
// It contains the functions for the main menu and pause menu in-game. 
public class LevelLoader : MonoBehaviour
{
    // Call to animator component of "LevelLoader" object. Float "transitionDelay" can be edited to control speed of transition.
    public Animator transition;
    public float transitionDelay;

    // When loading the next level, build the scene manager index and increment it by one.
    public void NewGame ()
    {
        SaveSystem.Delete();
        StartCoroutine(LoadLevel(1));
    }

    // When loading a saved game, get the integer value of the PlayerPrefs "SavedState". This int value is the level the user saved their game on.
    public void LoadGame (int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    // Function used for the level select menu.
    public void SelectLevel (int level)
    {
        // If the user wants to load a specific level instead, their saved game will be overwritten. 
        SaveSystem.Delete();
        // Load level selected by user.
        StartCoroutine(LoadLevel(level));
    }

    // Function used to load the next level of the game. 
    public void LoadNextLevel ()
    {
        // Load next level.
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        // Clear the list of stars saved by the SpeechManager GameObject. This is because it will store the stars of the next level now. 
        SpeechManager.stars.Clear();
    }

    // Function to load the main menu of the game.
    public void LoadMenu ()
    {
        StartCoroutine(LoadLevel(0));
        Time.timeScale = 1f;
    }

    // Function to call boolean trigger of animator and run transition on-screen.
    IEnumerator LoadLevel (int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionDelay);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }

}
