using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);

        bgmSlider.value = PlayerPrefs.GetFloat("BGM_VOLUME", 0.8f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME", 0.8f);
    }
}
