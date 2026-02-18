using UnityEngine;
using TMPro;

public class CurrencyHUD : MonoBehaviour
{
    public TMP_Text currencyText;

    void Update()
    {
        if (CurrencyManager.Instance == null)
            return;

        currencyText.text = $"{CurrencyManager.Instance.Currency}";
    }
}
