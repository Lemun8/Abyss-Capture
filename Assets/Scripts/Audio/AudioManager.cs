using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer;

    [Header("Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    const string BGM_KEY = "BGM_VOLUME";
    const string SFX_KEY = "SFX_VOLUME";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumes();
    }

    // ---------- PLAYBACK ----------
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // ---------- VOLUME ----------
    public void SetBGMVolume(float value)
    {
        audioMixer.SetFloat("BGMVolume", LinearToDB(value));
        PlayerPrefs.SetFloat(BGM_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", LinearToDB(value));
        PlayerPrefs.SetFloat(SFX_KEY, value);
    }

    void LoadVolumes()
    {
        float bgm = PlayerPrefs.GetFloat(BGM_KEY, 0.8f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 0.8f);

        SetBGMVolume(bgm);
        SetSFXVolume(sfx);
    }

    float LinearToDB(float value)
    {
        return value <= 0.001f ? -80f : Mathf.Log10(value) * 20f;
    }
}
