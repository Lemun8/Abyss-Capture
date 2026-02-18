using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthMeterUI : MonoBehaviour
{
    [Header("UI")]
    public Slider depthSlider;
    public RectTransform handleRect;
    public RectTransform depthTextRect;
    public TextMeshProUGUI depthText;

    private DepthTracker depthTracker;
    private LevelData levelData;

    void OnEnable()
    {
        LevelManager.OnPlayerSpawned += OnPlayerReady;
    }

    void OnDisable()
    {
        LevelManager.OnPlayerSpawned -= OnPlayerReady;
    }

    void Start()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            levelData = lm.GetLevelData();
    }

    void OnPlayerReady(GameObject player)
    {
        depthTracker = player.GetComponent<DepthTracker>();
    }

    void Update()
    {
        if (depthTracker == null || levelData == null) return;
        UpdateDepthMeter();
    }

    void UpdateDepthMeter()
    {
        float normalizedDepth = Mathf.InverseLerp(
            levelData.minDepth,
            levelData.maxDepth,
            depthTracker.CurrentDepth
        );

        depthSlider.value = normalizedDepth;
        depthText.text = $"{Mathf.RoundToInt(depthTracker.CurrentDepth)} m";

        UpdateTextPosition();
    }

    void UpdateTextPosition()
    {
        depthTextRect.position = handleRect.position + new Vector3(90f, 0f, 0f);
    }
}
