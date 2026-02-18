using UnityEngine;

public enum UpgradeType
{
    Oxygen,
    Suit,
    Camera
}

[CreateAssetMenu(
    fileName = "NewUpgrade",
    menuName = "Abyss Capture/Upgrade"
)]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public UpgradeType type;

    public int maxLevel = 5;
    public int baseCost = 100;

    [Header("Upgrade Values")]
    public float valuePerLevel;

    public int GetCost(int currentLevel)
    {
        return baseCost * (currentLevel + 1);
    }
}
