using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Credit to Brackey's tutorial on YouTube (Link: https://youtu.be/JivuXdrIHK0). This script is responsbile for the pasue menu of the game.
public class PauseMenu : MonoBehaviour
{
    // Boolean variable which will indicate whether game is paused or not
    public static bool gamePaused;

    // Store pause menus and its relevant menus (GameObjects)
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject quitMenu;

    // Store GUI GameObjects
    public GameObject waypoints;
    public GameObject scoreUI;
    public GameObject levelComplete;

    // Store reference to LevelLoader script
    public LevelLoader levelLoader;


    // Update is called once per frame
    void Update()
    {
        // If user clicks "Esc"...
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If game is paused, resume game, else pause it
            if(gamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    // Resume function
    private void Resume()
    {
        // Disable pause menu and its relevant menus
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);

        // Enable waypoints/score system UI
        waypoints.SetActive(true);
        scoreUI.SetActive(true);

        if(FindObjectOfType<ScoreSystem>().levelDone == 1)
        {
            levelComplete.SetActive(true);
        }

        // Set timescale to 1f
        Time.timeScale = 1f;

        // Unpause audio listener and set "gamePaused" boolean to false
        AudioListener.pause = false;
        gamePaused = false; 
    }

    // Pause function
    private void Pause()
    {
        // Enable pause menu
        pauseMenu.SetActive(true);

        // Disable waypoints/score system UI
        waypoints.SetActive(false);
        scoreUI.SetActive(false);
        levelComplete.SetActive(false);

        // Set timescale to 0f
        Time.timeScale = 0f;

        // Pause audio listener and set "gamePaused" boolean to true
        AudioListener.pause = true;
        gamePaused = true;
    }

    // Load main menu function
    private void LoadMenu()
    {
        // When the "Menu" button is clicked, call the LevelLoader function "LoadMenu"
        levelLoader.LoadMenu();
    }

    private void QuitGame()
    {
        // When the "Quit" button is clicked 
        Application.Quit();
    }
}
