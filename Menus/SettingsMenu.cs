using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Reference to objects responsible for adjusting sound volume, game quality, and resolution.
    public AudioMixer audioMixer;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    // Resolution array which will contain all resolutions user can change to.
    Resolution[] resolutions;

    // Start function will find suitable resolutions and populate the resolutions dropdown menu to suit the users monitor.
    void Start ()
    {
        // Resolutions will be populated with unique resolutions suitable for their monitor. 
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        // Clear current options.
        resolutionDropdown.ClearOptions();

        // Create a new list of strings which will contain all resolution options for the user. 
        List<string> options = new List<string>();

        // Declare an index for the resolution. 
        int currentResolutionIndex = 0;

        // Iterate through resolutions array and add each resolution to the list "options". 
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // If the user's current resolution is equal to one of the options in the "resolutions" array. 
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                // Assign the int "currentResolutionIndex" the current value of i. This is in order to present the user's current resolution to them in the drop-down box. 
                currentResolutionIndex = i;
            }
        }

        // Add the list "options" to the dropdown box.
        resolutionDropdown.AddOptions(options);
        // Set the current value of the dropdown to the user's current resolution.
        resolutionDropdown.value = currentResolutionIndex;
        // Refresh the dropdown box to reflect the user's current resolution.
        resolutionDropdown.RefreshShownValue();

        // PlayerPrefs will store the current volume level of the game. 
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    // Function which will change resolution depending on user choice.
    public void SetResolution (int resolutionIndex)
    {
        // Variable will store resolution value chosen by user.
        Resolution resolution = resolutions[resolutionIndex];
        // Call Screen.SetResolution to change the resolution of the game. 
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    // Function which will adjust game volume depending on user choice.
    public void SetVolume (float volume)
    {
        // Set game volume to level chosen by user.
        audioMixer.SetFloat("volume", volume);
        // Save level chosen so that it does not change when loading new scenes. 
        PlayerPrefs.SetFloat("volume", volume);
    }

    // Function which will change quality settings depending on user choice.
    public void SetQuality (int qualityIndex)
    {
        // Quality index will be equal to the option chosen by user. 
        qualityIndex = qualityDropdown.value;
        // Change the quality settings depending on which value the user chooses. 
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
}
