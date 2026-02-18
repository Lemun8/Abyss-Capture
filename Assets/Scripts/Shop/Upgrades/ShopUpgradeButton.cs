using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUpgradeButton : MonoBehaviour
{
    [Header("Setup")]
    public UpgradeType upgradeType;

    [Header("UI")]
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text costText;
    public Button button;

    UpgradeData data;

    void Awake()
    {
        data = UpgradeDatabase.Instance.GetUpgrade(upgradeType);
        button.onClick.AddListener(BuyUpgrade);
    }

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        int currentLevel = UpgradeManager.Instance.GetLevel(upgradeType);

        nameText.text = data.upgradeName;
        levelText.text = $"Level {currentLevel}/{data.maxLevel}";

        if (currentLevel >= data.maxLevel)
        {
            costText.text = "MAX";
            button.interactable = false;
            return;
        }

        int cost = data.GetCost(currentLevel);
        costText.text = cost.ToString();

        button.interactable =
            CurrencyManager.Instance.Currency >= cost;
    }

    void BuyUpgrade()
    {
        int currentLevel = UpgradeManager.Instance.GetLevel(upgradeType);
        int cost = data.GetCost(currentLevel);

        if (CurrencyManager.Instance.Currency < cost)
            return;

        CurrencyManager.Instance.SpendCurrency(cost);
        UpgradeManager.Instance.Upgrade(data);

        Refresh();
    }
}
