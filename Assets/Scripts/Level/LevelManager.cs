using UnityEngine;
using Cinemachine;
using System;

public class LevelManager : MonoBehaviour
{
    public static Action<GameObject> OnPlayerSpawned;

    public LevelData levelData;
    public Transform playerSpawnPoint;
    public CinemachineVirtualCamera virtualCamera;

    private GameObject playerInstance;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        playerInstance = Instantiate(
            levelData.playerPrefab,
            playerSpawnPoint.position,
            Quaternion.identity
        );

        AttachCamera(playerInstance.transform);

        OnPlayerSpawned?.Invoke(playerInstance);
    }

    void AttachCamera(Transform player)
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera not assigned!");
            return;
        }

        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }
}
