using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to Brackey's tutorial for providing guidance on how to create a save system (Link: https://youtu.be/XOjd_qU2Ido). 
// [System.Serializable] allows for the data below to be converted into bytes to be stored onto the user's device. 
[System.Serializable]
public class PlayerData
{
    // References to variables which will store the data of the user's current game. 
    public int level;
    public int currentScore;
    public List<string> stars;
    public float[] position; 

    // Create object PlayerData which will be used to create a saved file with the user's relevant data when they save their game progress. 
    public PlayerData (Player player)
    {
        // Assign each variable with the values of the player, i.e. the current level they are on, their current score, stars completed, and position. 
        level = player.level;
        currentScore = player.currentScore;
        stars = player.stars;

        // Initiliase the variable poisiton to a float array of size 3. This will contain the user's x, y, and z coordinates in-game. 
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
