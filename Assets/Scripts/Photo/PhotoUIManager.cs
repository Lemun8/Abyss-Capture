using UnityEngine;

public class PhotoUIManager : MonoBehaviour
{
    public static PhotoUIManager Instance;

    public RectTransform photoFrame;

    void Start()
    {
        int level = UpgradeManager.Instance.cameraLevel;
        UpgradeData cameraUpgrade = UpgradeDatabase.Instance.GetUpgrade(UpgradeType.Camera);

        photoFrame.sizeDelta += Vector2.one * (level * cameraUpgrade.valuePerLevel);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
