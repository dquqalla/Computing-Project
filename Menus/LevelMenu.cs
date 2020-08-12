using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject warningPrompt;

    private int chosenLevel;

    public void SelectLevel(int level)
    {
        // If a saved file exists.
        if (File.Exists(Application.persistentDataPath + "/player.speech"))
        {
            chosenLevel = level;
            // Set main menu to false to present warning prompt that a saved file exists.
            levelMenu.SetActive(false);
            // Set "WarningPrompt" to true to indicate to the user that they already have a saved file. If they continue, the saved file will be deleted and a new game will start. 
            warningPrompt.SetActive(true);
        }
        // Else, if a saved file does not exist, start a new game for the user. 
        else
        {
            FindObjectOfType<LevelLoader>().SelectLevel(level);
        }
    }

    public void LoadSelectLevel()
    {
        FindObjectOfType<LevelLoader>().SelectLevel(chosenLevel);
    }
}
