# Project 2 Development User Guide.
####  Game Flow
The game flow is broken up into 4 scenes.

**Scene 1: Load Scene**. This scene is automatically loaded when the game boots and will show a splash screen. After a preset amount of time (Adjustable through the scene's Scene Manager) the scene will automatically continue on to the Intro Scene. the players can also press space bar to skip the waiting time on the loading screen.

*  **Scene Manager (Object)**  
    * **Load Scene Timer**  
        * Determines the amount of time in seconds before the Intro Scene will be launched.

**Scene 2: Intro Scene**. This scene will automatically play the intorduction video. (This can be changed inside the scene's Movie Screen.) After a predertermined amount of time (Adjustable through the scene's Scene Manager) the player will be prompted to press space bar to continue to the main game.

* **Scene Manager (Object)**
    * **Intro Scene Timer**
        * Determines the amount of time in seconds before the gameplay Scene can be launched.
    * **Automatic Scene Continuation**
        * A checkbox that if checked will automatically trigger the gameplay scene after the intro scene timer reaches 0. Otherwise will display a prompt for the player to press space bar to continue.

* **Movie Screen(Object)**
    * **Video Clips**
        * A List of videos that the movie screen can play. The screen on the into scene is set to auto play, so simply change the videoclip inside the list to your prefered video.

**Scene 3: Gameplay Scene**. This scene is broken into 4 game states, each triggering one another through either preset timers or player input, the flow is as follows:

* **Input Incident**: This state will activate the incident input field and await the players input. After the input has been given, the play incident state is triggered.

* **Play Incident**: This state well play a play an incident video based on your Incident Input	Logic. Input fields will be disabled until the duration of the video has been taken. (The duration of a video clip needs to be entered manually on the video clip object) After the duration of the incident video, the responce input fiel will activate and the game will 	transition to the next state.

* **Input Response**: This state is similar to the incident input state except it will run the input against the Response Input Logic script and play that video instead.

* **Play Response**: Similar to the Play Incident state, the Response video will be played, and	at the end of the set duratoin a new incident inputfield will be activated and restart the cycle. 

*This cycle happens 5 times be default. (Can be changed in the GameplayManager on the Gameplay Scene)*

**Scene 4: Outro Scene**. After all the incidents have been responded to the scene will change to the outro scene and play the outro videoclip based on the logic inside of the Outro Video Logic script. After the duration has concluded the player will be prompted to press space to start the game again, this will then loop back to the into scene.

#### Importing Video
1. Convert video to theora OGG format.
2. Drag video file into the "Videos" Folder in the project window.
3. Duplicate an existing Video Clip Prefab.
4. Expand video file that you have just dragged into the project window. Then drag the video and audio components to the new video clip prefab.
5. Input video Name/ID and duration.
6. Drag the new video clip prefab into the "Video Clips" List on the movie screen object in the Gameplay Scene.

**Creating Responses**
1. Duplicate an existing Reponse Prefab.
2. Fill out the new values. (Video ID currently exists to be used as a 1 to 1 responce to video look up.)
3. Drag the new response into the responses list on the movie screen object.

**Creating Incidents**
1. Duplicate an existing Incident Prefab.
2. Fill out the new values. (Video ID currently exists to be used as a 1 to 1 incident to video look up.)
3. Drag the new incident into the responses list on the movie screen object.

**High Level Developer Scripts.**
Assuming that you are using this basic game flow, you will only need to interact with a few scripts. Each has been created for the express intent of letting you decide what videos play at what times and how the players actions impact the game, away from the internals of the system intself. Less fear of deleting something that is fundamental to the game working.

**IncidentInputLogic**
* This script is used to determine what incident card to activate based on player input. It current checks the players input against the `Incident.ID` and returns the first result for the list of possible incidents. This script may or may not be suitable to your game flow.

**ResponseInputLogic:**
* This script is used to determine what response card to activate based on player input. It current checks the players input against the `Response.ID` and returns the first result for the list of possible responses. This script may or may not be suitable to your game 	flow.

**VideoInputLogic** (*This script has 2 functions*)
* One for incidents and the other for responses. It is used to determine what video to play when the player gives input. The information the player will need in order to make these decisions can be found in `GameplayManager.instance` and
`GameManager.instance`. both of these objects are startic and globally accessable. The scripts come with 2 lines grabbing the current Response and current Incident as this will be where most of your variables will be coming from.

**OutroVideoLogic**
* Similar to `VideoInputLogic`, but it run once at the start of the Outro Scene. The Example logic takes the global score for the players and determines a video based on that.

**Incident & Response**
* These scripts contain variables that may be unique to each card, including points. And also contain a single function `Activate()` that is run once when the card is input through the input fields. The example logic given adds the points on each incident and response to a global score tracked over the course of the game, that determines the final video.

# WARNING!!!!!
**Due to time restraints, and wanting this to get you guys tonight there is VERY FEW null checks. Especially where the player input is concerned. Right now if they input something that can be matched it will work, otherwise throw a null. Although this has not been shown to break the games flow, it may well in the future. I will be working on this and
may release another version soon.**