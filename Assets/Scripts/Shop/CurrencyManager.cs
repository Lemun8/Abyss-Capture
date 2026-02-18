using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    const string CURRENCY_KEY = "CURRENCY_TOTAL";

    public int Currency { get; private set; }

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

    public void AddCurrency(int amount)
    {
        Currency += amount;
        Save();
    }

    public bool SpendCurrency(int amount)
    {
        if (Currency < amount)
            return false;

        Currency -= amount;
        Save();
        return true;
    }

    void Save()
    {
        PlayerPrefs.SetInt(CURRENCY_KEY, Currency);
        PlayerPrefs.Save();
    }

    void Load()
    {
        Currency = PlayerPrefs.GetInt(CURRENCY_KEY, 0);
    }
}
