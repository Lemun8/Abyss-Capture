using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonBinder : MonoBehaviour
{
    public PauseManager pauseManager;

    [Header("Pause Menu Buttons")]
    public Button continueButton;
    public Button settingsButton;
    public Button exitButton;

    [Header("Settings Menu")]
    public Button settingsBackButton;

    void Awake()
    {
        continueButton.onClick.AddListener(pauseManager.Resume);
        settingsButton.onClick.AddListener(pauseManager.OpenSettings);
        exitButton.onClick.AddListener(pauseManager.ExitToHub);

        settingsBackButton.onClick.AddListener(pauseManager.CloseSettings);
    }
}
