# Dissertation Project
**Blog: https://dquqa001.wordpress.com/**

Repository of my third-year Computer Science dissertation project, "Speech Therapy Game for Childhood Apraxia of Speech". 

Here you can find all the scripts that was used for my project. These scripts have been fully commented to explain their purpose. 

Whilst the majority of the code has been self-written, I have used a few online resources such as Unity's documentation and YouTube tutorials to guide me. These resources have been credited in the comments. I could not thank them enough for sharing their knowledge and expertise. 

Explaination of each folder and their relevant scripts can be found below: 

**Audio**

This folder contains the scripts which will handle the in-game audio. The *Sound* script defines the structure of a Sound object which is then passed into the *AudioManager* script. The *AudioManager* script contains all the functions to play and stop audio clips when necessary. 

**Dialogue**

This folder contains the scripts that will display the on-screen dialogue to the user. The *Dialogue* script defines the structure of each sentence within a dialogue. The sentences for the dialogue for each collectable is stored in a string array. Each collectable within a level has a *DialogueTrigger* script attached to it. The *DialogueTrigger* script is responsible for calling the appropriate methods and logic whenever the player enters/exits the collectable's trigger. 

When a user triggers a collectable, the string array containing the sentences is passed to the *DialogueManager* script which contains the functions required to display the text on the user's screen and call the SpeechManager script at the end of the dialogue. 

The *SpeechManager* script is what controls the speech-recognition aspect of the game. Whenever a user approaches a collectable, and proceeds with the dialogue until they reach the end, speech-recognition is enabled waiting for the user's verbal input. 

**Menus**

This folder contains the scripts responsible for the functions for each menu, i.e., start menu, level select menu, settings menu, and the pause menu. These scripts contain methods to start a new game, save the current game, quit the game, and change settings of the game such as graphics and resolution. 

**Save & Load**

This folder contains the scripts which can save the user's current data within the game and load it for future use. The *Player* is attached to the game character game object. When a user clicks save, the player's relevant data is saved and the SaveSystem function *SavePlayer* is called. The *SavePlayer* function makes use of the BinaryFormatter module to serialise a new PlayerData object. 

The *PlayerData* script simply defines the structure of a PlayerData object which will be used to store the user's player data, i.e., the level they are currently on, their position, tasks completed, whenever they complete a task (autosave) or choose to save their progress via the pause menu. 

**Other**

This folder contains other scripts for controlling the behaviour of the game. The *AutoSave* script will play an animation indicating to the user that the game is saving whenever they complete a task.

The *LastScene* script is solely responsible for the buttons in the last scene of the game (the level which informs the user they have completed all levels). 

The *StartGame* script determines whether the user requested to start a new game or load an existing one, and whether or not to load the user's saved game data or load the default values for the level. 

The *Waypoints* script is used to display the remaining distance between the user and all collectables within a level. 

**Score System**

The *ScoreSystem* script controls the behaviour for whenever a user completes a certain task. It contains the functions to increment the score whenever the user completes a task. It also contains the logic required to determine whether to load the user's saved score if they were to load an existing game or reset it back to zero. 







