using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This script is for providing on-screen waypoints indicating the distance of each star relative to the user's position in each level.
// Credits to Partrum Game Tutorials for providing a video tutorial on how to implement this function (Link: https://youtu.be/mT3ggeU82f0).
public class Waypoints : MonoBehaviour
{
    // Text containing the current distance between the user and a star. 
    private TextMeshProUGUI distanceText;

    // Will store the coordinates of the player and targets (i.e. stars).
    public Transform player;
    public Transform target;

    // Float which will dictate how close user has to be to a star for waypoint to disappear. 
    public float closeEnoughDist;
    // Offset which will adjust the positioning of the star. 
    public Vector3 offset;

    // Function called at the start of the game. Obtain TextMeshProUGUI component which will be amended to reflect the current distance.
    private void Start()
    {
        distanceText = GetComponent<TextMeshProUGUI>();
    }

    // Update function which will be called every frame of the game. 
    private void Update()
    {
        // If the target is NOT null call functions GetDistance() and CheckOnScreen(). 
        if(target != null)
        {
            GetDistance();
            CheckOnScreen();
        } 
        // Else, the waypoint has no target, so disable it. 
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    // Function which works out the distance of the player to a star. 
    private void GetDistance()
    {
        // Declare float dist and work out distance between players position and the star position.
        float dist = Vector3.Distance(player.position, target.position);
        // Convert the float to string to present on-screen.
        distanceText.text = dist.ToString("N0") + "m";

        // If the user is close enough to the waypoint, destroy the waypoint. 
        if (dist < closeEnoughDist)
        {
            Destroy(gameObject);
        }
    }

    // Function which will check whether the user is looking at a star on-screen and whether or not the distance should be displayed to the user. 
    private void CheckOnScreen()
    {
        // Float value which will represent whether the star is in the user's field of view. 
        float onScreen = Vector3.Dot((target.position - Camera.main.transform.position).normalized, Camera.main.transform.forward);

        // If it is less than or equal to 0, the star is not in the user's field of view, so call ToggleUI() function passing a false boolean in the parameter.
        if (onScreen <= 0)
        {
            ToggleUI(false);
        } 
        // Else, call the ToggleUI() function and pass a true boolean value in the parameter. 
        else
        {
            ToggleUI(true);
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    // Function which will determine whether or not to set the distance text object to active or not. 
    private void ToggleUI(bool _value)
    {
        distanceText.enabled = _value;
    }
}
