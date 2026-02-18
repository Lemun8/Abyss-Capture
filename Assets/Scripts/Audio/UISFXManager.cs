using UnityEngine;

public class UISFXManager : MonoBehaviour
{
    public static UISFXManager Instance;

    public AudioSource audioSource;
    public AudioClip hoverClip;
    public AudioClip clickClip;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverClip);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip);
    }
}
