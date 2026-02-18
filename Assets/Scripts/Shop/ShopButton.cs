using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;

    [Header("Buttons")]
    public Button[] toggleButtons;

    void Awake()
    {
        foreach (Button btn in toggleButtons)
        {
            btn.onClick.AddListener(ToggleShop);
        }
    }

    public void ToggleShop()
    {
        bool isOpen = shopPanel.activeSelf;
        shopPanel.SetActive(!isOpen);
    }
}
