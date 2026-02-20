# Abyss-Capture
The Code Design Documentation for Abyss Capture

<img src="https://github.com/user-attachments/assets/7bb022c3-fbd8-4b70-bbeb-dddcfd148925"/>
<!-- About the game -->
<td width="70%" valign="top" style="padding:15px;">
  <h2>About </h2>
  <p style="max-width:700px;">
    Abyss Capture is a educational game that lets the player discover creatures in the sea. The player controls a diver with a goal of documenting all of the creature available inside the levels, the player then can read real life facts about the creatures documented inside the encyclopedia.
  </p>
  <a href="https://janshenfung.itch.io/abyss-capture">
    <img src="https://img.shields.io/badge/Itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white" />
  </a>
</td>


##  Scripts and Features
Here are some of the main script that is used to manage the game.
<br>
**Player Systems**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `PlayerUnderwaterMovement.cs` | Controls underwater physics-based player movement using acceleration and max speed limits. Integrates with the Animator to blend swimming animations based on movement direction and velocity magnitude. |
| `PlayerOxygen.cs` | Manages the player's oxygen resource with dynamic drain rates based on activity state (idle, moving, or photo mode). Applies upgrade bonuses from the shop system and forces the player to surface when oxygen depletes, triggering game-over logic. |
| `DepthTracker.cs`  | Tracks the player's current depth underwater, used by the creature spawning system to determine which species can appear at different depth levels. |
<br>

<br> **Photo Capture System**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `PhotoModeController.cs` | Handles entering and exiting photo mode via spacebar input, activating the camera frame UI and modifying oxygen drain rates. Respects pause state to prevent photo mode during paused gameplay. |
| `PhotoCapture.cs` | Detects creatures within the photo frame using 2D physics overlap detection when the player clicks to take a photo. Triggers creature documentation and sends frame data to the screenshot system. |
| `PhotoScreenshot.cs`  | Captures actual screenshots of photographed creatures and saves them to persistent storage, only if the player has upgraded their camera to level 5 or higher. |
<br>

<br> **Creature Systems**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `Creature.cs` | Represents individual creatures with ScriptableObject data. Handles being photographed by unlocking entries in the encyclopedia, awarding score points, registering with the star tracker, and triggering screenshot capture if camera upgrades allow. |
| `CreatureAI.cs` | Implements simple AI with wander and flee behaviors. Creatures swim in random directions, changing course periodically, and flee when the player gets too close. Uses sprite flipping and rotation for natural swimming animations. |
| `CreatureSpawner.cs` | Spawns creatures dynamically just outside the camera view based on current depth level and spawn tables. Manages population limits, validates spawn positions against level bounds and collisions, and tracks active creature count. |
| `CreatureData.cs` |  (ScriptableObject) Defines creature properties including species ID, display name, photo score value, and visual sprite data. |
| `CreatureSpawnTable.cs` | (ScriptableObject) Configuration for depth-based creature distribution, allowing different species to appear at specific depth ranges for environmental variety. |
<br>

<br> **Level Management**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `LevelManager.cs` | Handles level initialization by spawning the player at designated spawn points and configuring Cinemachine camera following. Broadcasts player spawn events for other systems to respond to. |
| `LevelData.cs` | (ScriptableObject) Contains level-specific configuration including player prefab, spawn tables, level ID, credit rewards, and other metadata. |
| `ScoreManager.cs` | Tracks player score with passive accumulation over time and bonus points from photographing creatures. Automatically pauses when time scale is zero and finalizes score when oxygen depletes. |
| `LevelStarTracker.cs` | Awards 1-3 stars based on percentage of unique species documented in a level (1 star for any documentation, 2 stars for 60%, 3 stars for 100%). Saves star progression and grants currency rewards for new stars earned. |
<br>

<br> **Progression Systems**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `UpgradeManager.cs` | Persistent singleton that manages three upgrade tracks: Oxygen (extends air supply), Suit (increases depth resistance), and Camera (unlocks photo saving at level 5). Uses PlayerPrefs for save data and broadcasts upgrade events. |
| `CurrencyManager.cs` | Persistent singleton managing the in-game currency system. Handles earning currency from stars and spending it on upgrades with validation to prevent negative balances. |
| `EncyclopediaManager.cs` | Persistent encyclopedia system that tracks discovered creatures and their photograph paths. Loads saved screenshots from disk and converts them to sprites for UI display. Uses PlayerPrefs for persistent creature discovery data. |
<br>

<br> **UI & Menus**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `MainMenuController.cs` | Main menu handler with options for Continue (loads hub), New Game (wipes save data), Settings panel toggle, and Exit. Provides the entry point to the game experience. |
| `PauseManager.cs` | In-game pause system triggered by Escape key. Freezes time, displays pause UI, manages settings panel transitions, and provides functionality to exit back to the hub scene. |
<br>

<br> **Audio**:
|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `AudioManager.cs` | Centralized audio controller managing BGM and SFX playback through Unity's Audio Mixer. Converts linear volume sliders to logarithmic decibel values and persists volume preferences using PlayerPrefs. |
<br>

##  System Design
Other than individual scripts, the game relies on several interconnected systems to handle core mechanics. Below is an overview of how the main gameplay loops are engineered.

#### 1. Physics-Based Photo Capture Detection
Instead of relying on raycasts or trigger colliders, the photo system uses dynamic frame-based physics queries to detect creatures within the camera viewfinder in real-time.

*   **How it works:** PhotoCapture.cs calculates the world-space bounds of the UI photo frame using RectTransform.GetWorldCorners(), then performs a Physics2D.OverlapBoxAll() query to detect all creatures within that rectangular area when the player clicks.
*   **Frame Precision:** The system converts screen-space UI corners to world-space coordinates, creating an accurate detection zone that perfectly matches what the player sees in the viewfinder, eliminating mismatches between visual feedback and capture logic.
  
#### 2. Depth-Based Dynamic Spawning
The creature spawning system creates an adaptive ecosystem by spawning different species based on the player's current depth, simulating realistic ocean biome distributions.

*   **How it works:** CreatureSpawner.cs queries the player's DepthTracker every spawn interval, then uses CreatureSpawnTable.GetRandomPrefabForDepth() to select species appropriate for that depth range, ensuring shallow water creatures don't appear in the abyss.
*   **Off-Screen Spawning:** Creatures spawn just outside the camera's viewport using Camera.ViewportToWorldPoint() calculations with a configurable buffer zone, making creatures appear naturally as the player explores rather than popping in visibly.
*   **Population Control:** The spawner maintains a maxActiveCreatures limit and validates spawn positions against level bounds and collision layers before instantiation, preventing overcrowding and invalid placements.
