using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "Abyss Capture/Creature Spawn Table")]
public class CreatureSpawnTable : ScriptableObject
{
    public List<CreatureSpawnEntry> creatures;

    public GameObject GetRandomPrefabForDepth(float depth)
    {
        var valid = creatures
            .Where(c =>
                depth >= c.creatureData.minSpawnDepth &&
                depth <= c.creatureData.maxSpawnDepth)
            .ToList();

        if (valid.Count == 0)
            return null;

        return valid[Random.Range(0, valid.Count)].creaturePrefab;
    }
}
