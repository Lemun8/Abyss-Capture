using UnityEngine;

public class UpgradeDatabase : MonoBehaviour
{
    public static UpgradeDatabase Instance { get; private set; }

    public UpgradeData[] upgrades;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public UpgradeData GetUpgrade(UpgradeType type)
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.type == type)
                return upgrade;
        }

        Debug.LogError("Upgrade not found: " + type);
        return null;
    }
}
