using UnityEngine;

public class CreatureLifeTracker : MonoBehaviour
{
    CreatureSpawner spawner;

    public void Init(CreatureSpawner creatureSpawner)
    {
        spawner = creatureSpawner;
    }

    void OnDestroy()
    {
        if (spawner != null)
            spawner.NotifyCreatureDestroyed();
    }
}
