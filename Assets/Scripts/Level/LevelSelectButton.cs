using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    [Header("Level Info")]
    public string sceneName;
    public int requiredSuitLevel = 1;

    [Header("UI")]
    public Button button;
    public TMP_Text labelText;
    // public GameObject lockIcon;

    [Header("Stars")]
    public Image[] starImages;
    public Sprite starFilled;
    public Sprite starEmpty;
    public LevelData levelData;

    void Start()
    {
        // Safety check
        if (UpgradeManager.Instance == null)
        {
            Debug.LogError("UpgradeManager not found in scene!");
            return;
        }

        UpgradeManager.OnUpgradeChanged += OnUpgradeChanged;

        Refresh();
        button.onClick.AddListener(OnClick);
    }

    void OnDestroy()
    {
        if (UpgradeManager.Instance != null)
            UpgradeManager.OnUpgradeChanged -= OnUpgradeChanged;

        button.onClick.RemoveListener(OnClick);
    }

    void Refresh()
    {
        if (UpgradeManager.Instance == null)
            return;

        int suitLevel = UpgradeManager.Instance.suitLevel;
        bool unlocked = suitLevel >= requiredSuitLevel;

        button.interactable = unlocked;
        // lockIcon?.SetActive(!unlocked);

        int stars = LevelStarSave.GetStars(levelData.levelID);

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? starFilled : starEmpty;
            starImages[i].gameObject.SetActive(unlocked);
        }

        labelText.text = unlocked
            ? levelData.levelName
            : $"Locked (Suit Lv {requiredSuitLevel})";
    }

    void OnClick()
    {
        if (!button.interactable)
            return;

        SceneManager.LoadScene(sceneName);
    }

    void OnUpgradeChanged(UpgradeType type)
    {
        if (type == UpgradeType.Suit)
            Refresh();
    }
}
