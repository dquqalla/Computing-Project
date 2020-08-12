using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Credit to Brackey's tutorial for providing guidance on how to create a save system (Link: https://youtu.be/XOjd_qU2Ido). 
// This script is the save system used to create BinaryFormatter and FileStream objects used to stream data from the game and convert it into binary to be stored on the user's device. 
public static class SaveSystem
{
    // Function used to save the player's current game. 
    public static void SavePlayer (Player player)
    {
        // Create a new BinaryFormatter object.
        BinaryFormatter formatter = new BinaryFormatter();
        // Define the path that will store the saved file. The use of "Application.persistenDataPath" will ensure the game can be saved on any device regardless if it is Windows or MacOS.
        string path = Application.persistentDataPath + "/player.speech";
        // Create a new FileStream object which will read the user's data and create a new file using "FileMode.Create".
        FileStream stream = new FileStream(path, FileMode.Create);

        // Create a new PlayerData object data. This will contain the saved file data.
        PlayerData data = new PlayerData(player);

        // Call the binary formatter to stream the data from the FileStream object into the PlayerData object.
        formatter.Serialize(stream, data);
        // Close the stream and save the file to the user's device.
        stream.Close();
    }

    // Function used to load the user's game.
    public static PlayerData LoadPlayer ()
    {
        // Define the path of the user's saved file.
        string path = Application.persistentDataPath + "/player.speech";

        // If a file exists in that specified path...
        if (File.Exists(path))
        {
            // Create a new BinaryFormatter and FileStream object to read the file.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            // Create a PlayerData object and assign it the data of the saved file.
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            // Close the stream.
            stream.Close();
            
            // Return the saved data.
            return data;

        } 
        // Else...
        else
        {
            // Return null as no saved file exists.
            return null;
        }
    }

    // Function called to delete a saved file whenever a user stars a new game.
    public static void Delete()
    {
        File.Delete(Application.persistentDataPath + "/player.speech");
    }
}
