using UnityEngine;

public enum DepthZone
{
    Lit,
    Twilight,
    Midnight
}

[CreateAssetMenu(
    fileName = "NewCreatureData",
    menuName = "Abyss Capture/Creature Data"
)]
public class CreatureData : ScriptableObject
{
    [Header("Identity")]
    public string creatureID;          // e.g. "anglerfish_01"
    public string displayName;          // Angler Fish

    [Header("Encyclopedia")]
    [TextArea(3, 6)]
    public string description;

    [Header("Scoring")]
    public int photoScore = 50;

    [Header("Spawn Depth Range")]
    public float minSpawnDepth;
    public float maxSpawnDepth;

    [Header("Encyclopedia (Locked State)")]
    [TextArea(2, 4)]
    public string lockedDescription = "This creature has not been documented yet.";
    public Sprite lockedPlaceholder;

    [Header("Encyclopedia")]
    public DepthZone depthZone;
    
    [Header("Real Life Reference")]
    public Sprite realLifeImage;

    [Header("Photo Rules (Optional, later)")]
    public float minPhotoSize = 0.2f;   // % of frame (future use)
}
