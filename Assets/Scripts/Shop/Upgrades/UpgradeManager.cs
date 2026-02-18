using UnityEngine;
using System;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public static System.Action<UpgradeType> OnUpgradeChanged;

    const string OXYGEN_KEY = "UPGRADE_OXYGEN";
    const string SUIT_KEY = "UPGRADE_SUIT";
    const string CAMERA_KEY = "UPGRADE_CAMERA";

    public int oxygenLevel;
    public int suitLevel;
    public int cameraLevel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public int GetLevel(UpgradeType type)
    {
        return type switch
        {
            UpgradeType.Oxygen => oxygenLevel,
            UpgradeType.Suit => suitLevel,
            UpgradeType.Camera => cameraLevel,
            _ => 0
        };
    }

    public void Upgrade(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeType.Oxygen:
                oxygenLevel++;
                break;
            case UpgradeType.Suit:
                suitLevel++;
                break;
            case UpgradeType.Camera:
                cameraLevel++;
                break;
        }

        Save();
        OnUpgradeChanged?.Invoke(data.type);
    }

    void Save()
    {
        PlayerPrefs.SetInt(OXYGEN_KEY, oxygenLevel);
        PlayerPrefs.SetInt(SUIT_KEY, suitLevel);
        PlayerPrefs.SetInt(CAMERA_KEY, cameraLevel);
        PlayerPrefs.Save();
    }

    void Load()
    {
        oxygenLevel = PlayerPrefs.GetInt(OXYGEN_KEY, 0);
        suitLevel = PlayerPrefs.GetInt(SUIT_KEY, 1);
        cameraLevel = PlayerPrefs.GetInt(CAMERA_KEY, 0);
    }
}
