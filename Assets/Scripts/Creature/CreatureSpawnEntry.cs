using UnityEngine;

[System.Serializable]
public class CreatureSpawnEntry
{
    public CreatureData creatureData;
    public GameObject creaturePrefab;

    [Range(1, 100)]
    public int weight = 10; // spawn chance 
}
