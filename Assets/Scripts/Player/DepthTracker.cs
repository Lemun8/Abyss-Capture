using UnityEngine;

public class DepthTracker : MonoBehaviour
{
    private LevelData levelData;

    public float CurrentDepth { get; private set; }

    void Start()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        levelData = lm.GetLevelData();
    }

    void Update()
    {
        UpdateDepth();
    }

    void UpdateDepth()
    {
        float t = Mathf.InverseLerp(
            levelData.worldTopY,
            levelData.worldBottomY,
            transform.position.y
        );

        CurrentDepth = Mathf.Lerp(
            levelData.minDepth,
            levelData.maxDepth,
            t
        );
    }
}
