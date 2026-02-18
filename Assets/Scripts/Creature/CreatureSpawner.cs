using System.Collections;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    [Header("Spawn Table")]
    public CreatureSpawnTable spawnTable;

    [Header("Spawn Control")]
    public int maxActiveCreatures = 15;
    public float spawnInterval = 2.5f;

    [Header("Camera Spawn Rules")]
    public Camera targetCamera;
    public float spawnBuffer = 2f;

    [Header("Level Bounds")]
    public BoxCollider2D levelBounds;

    [Header("Spawn Validation")]
    public LayerMask blockingLayers;
    public float spawnCheckRadius = 0.4f;

    DepthTracker depthTracker;

    int currentCreatureCount;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (levelBounds == null)
        {
            Debug.LogError("CreatureSpawner missing LevelBounds reference!");
            enabled = false;
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    void OnEnable()
    {
        LevelManager.OnPlayerSpawned += OnPlayerSpawned;
    }

    void OnDisable()
    {
        LevelManager.OnPlayerSpawned -= OnPlayerSpawned;
    }

    void OnPlayerSpawned(GameObject player)
    {
        depthTracker = player.GetComponent<DepthTracker>();
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentCreatureCount >= maxActiveCreatures)
                continue;

            TrySpawnCreature();
        }
    }

    void TrySpawnCreature()
    {
        if (depthTracker == null)
            return;

        Vector2 spawnPos;
        if (!TryGetValidSpawnPosition(out spawnPos))
            return;

        float currentDepth = depthTracker.CurrentDepth;

        GameObject prefab =
            spawnTable.GetRandomPrefabForDepth(currentDepth);

        if (prefab == null)
            return;

        GameObject creature = Instantiate(
            prefab,
            spawnPos,
            Quaternion.identity,
            transform
        );

        currentCreatureCount++;

        creature.GetComponent<CreatureLifeTracker>()
            ?.Init(this);
    }


    bool TryGetValidSpawnPosition(out Vector2 spawnPos)
    {
        const int maxAttempts = 10;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 candidate = GetSpawnPositionOutsideCamera();

            if (!IsInsideBounds(candidate))
                continue;

            if (!IsPositionClear(candidate))
                continue;

            spawnPos = candidate;
            return true;
        }

        spawnPos = Vector2.zero;
        return false;
    }

    Vector2 GetSpawnPositionOutsideCamera()
    {
        Vector3 camMin = targetCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 camMax = targetCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        camMin -= Vector3.one * spawnBuffer;
        camMax += Vector3.one * spawnBuffer;

        int side = Random.Range(0, 4);
        float x = 0, y = 0;

        switch (side)
        {
            case 0: x = camMin.x; y = Random.Range(camMin.y, camMax.y); break;
            case 1: x = camMax.x; y = Random.Range(camMin.y, camMax.y); break;
            case 2: x = Random.Range(camMin.x, camMax.x); y = camMax.y; break;
            case 3: x = Random.Range(camMin.x, camMax.x); y = camMin.y; break;
        }

        return new Vector2(x, y);
    }

    bool IsPositionClear(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(
            position,
            spawnCheckRadius,
            blockingLayers
        );

        return hit == null;
    }

    bool IsInsideBounds(Vector2 position)
    {
        return levelBounds.bounds.Contains(position);
    }

    public void NotifyCreatureDestroyed()
    {
        currentCreatureCount--;
    }

    void OnDrawGizmosSelected()
    {
        if (levelBounds != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                levelBounds.bounds.center,
                levelBounds.bounds.size
            );
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnCheckRadius);
    }
}
