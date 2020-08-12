using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script controls the functions of the main menu and its buttons. The main menu consists of a new game, load, options, and quit button. 
public class MainMenu : MonoBehaviour
{
    // Calls to objects within the main menu
    public GameObject menu;
    public GameObject warningPrompt;
    public GameObject noSavedGame;
    public Animator promptAnimation;

    PlayerData data;

    private void Start()
    {
        data = SaveSystem.LoadPlayer();
        promptAnimation.enabled = false;
    }

    // Function when user starts a new game.
    private void NewGame()
    {
        // If a saved file exists.
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
        {
            // Set main menu to false to present warning prompt that a saved file exists.
            menu.SetActive(false);
            // Set "WarningPrompt" to true to indicate to the user that they already have a saved file. If they continue, the saved file will be deleted and a new game will start. 
            warningPrompt.SetActive(true);
        }
        // Else, if a saved file does not exist, start a new game for the user. 
        else
        {
            FindObjectOfType<LevelLoader>().NewGame();
        }

    }

    // Function when user wants to load saved game.
    private void LoadGame ()
    {
        // If a saved file exists.
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
        {
            // Retrieve level saved from data file. 
            int level = data.level;
            // Pass int level in LoadGame function to load the user's last saved level. 
            FindObjectOfType<LevelLoader>().LoadGame(level);
        }
        // Else...
        else
        {
            promptAnimation.enabled = true;
            promptAnimation.Play("PopUp", -1, 0f);
        }
    }

    // Function called when user quits game.
    private void QuitGame ()
    {
        Application.Quit();
    }
}
