using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOxygenUI : MonoBehaviour
{
    public Slider oxygenSlider;
    public TMP_Text oxygenText;

    PlayerOxygen oxygen;

    void OnEnable()
    {
        PlayerOxygen.OnPlayerOxygenSpawned += Bind;
    }

    void OnDisable()
    {
        PlayerOxygen.OnPlayerOxygenSpawned -= Bind;
    }

    void Bind(PlayerOxygen playerOxygen)
    {
        oxygen = playerOxygen;
        oxygenSlider.maxValue = oxygen.maxOxygen;
        oxygenSlider.value = oxygen.CurrentOxygen;
    }

    void Update()
    {
        if (oxygen == null) return;

        oxygenSlider.value = oxygen.CurrentOxygen;
        oxygenText.text = $"{Mathf.CeilToInt(oxygen.CurrentOxygen)}%";
    }
}
