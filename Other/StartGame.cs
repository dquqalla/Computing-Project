using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is initiated when each level is first loaded. It contains the functions that will determine whether to load the user's saved file or start the level with its default values. 
public class StartGame : MonoBehaviour
{
    // References to player object and data object.
    public GameObject player;

    // Called when GameObject containing this script is loaded into scene. 
    private void Awake()
    {
        // Set vSync to 1. This is so that the games FPS will match the refresh rate of the user's monitor, allowing for smooth gameplay. 
        QualitySettings.vSyncCount = 1; 
        // Activate audio listener in order to hear in-game sounds. 
        AudioListener.pause = false;

        Cursor.visible = false; 
    }

    // Start function is initiated at the first-frame. 
    private void Start()
    {
        // If a saved file exists...
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
        {
            // Assign the variable "data" with the user's last save data. 
            PlayerData data = SaveSystem.LoadPlayer();

            // However, if the user loads a level not equal to saved data level load default values for player position, stars completed, etc. This is because the user might have just advanced to the next level rather than load their last saved game. 
            if (data.level != SceneManager.GetActiveScene().buildIndex)
            {
                return;
            }
            // Else, the user has chosen to load their last saved game...
            else
            {
                // Call LoadPosition + ActiveStars function to load saved data. 
                LoadPosition(data);
                ActiveStars(data);
                // Assign the list "stars" in the SpeechManager GameObject with the list in the data file. 
                SpeechManager.stars = data.stars;
            }
        }
        // If a saved file does not exist, load level with default values. 
        else
        {
            SpeechManager.stars.Clear();
            return;
        }
            
    }

    // Function to load position of player when they last saved their game.
    private void LoadPosition (PlayerData data)
    {
        // Create Vector3 object which will store coordinates stored in saved file of the user's last saved position. 
        Vector3 position;
        // Assign each position with their corresponding coordinate.
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        // Assign player saved position coordinates. 
        player.transform.position = position;
    }

    // Function which iterates through completed star arrays in saved file. This will be used to remove completed stars from the level that the user completed when they last saved their game. 
    private void ActiveStars (PlayerData data)
    {
        // Iterate through the strings stored within the stars array in the user's saved file. 
        foreach (string star in data.stars)
        {
            // Find any star GameObject with the same tag as the current string and store it in a GameObject array.
            GameObject[] stars = GameObject.FindGameObjectsWithTag(star);

            // Iterate through the GameObject array and set each star to false as they have already been completed when the user last saved their game. 
            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(false);
            }
        }

    }
}
