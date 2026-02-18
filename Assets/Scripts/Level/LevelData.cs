using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Abyss/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelID;
    public string levelName;

    [Header("Depth Settings")]
    public float minDepth;
    public float maxDepth;

    [Header("World Mapping")]
    public float worldTopY;
    public float worldBottomY;

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Creatures")]
    public CreatureSpawnTable spawnTable;

    [Header("Star System")]
    public int creditRewardPerStar = 500;
}
