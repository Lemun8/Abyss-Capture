using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonBinder : MonoBehaviour
{
    public MainMenuController controller;

    public Button continueButton;
    public Button newGameButton;
    public Button settingsButton;
    public Button exitButton;
    public Button settingsBackButton;

    void Awake()
    {
        continueButton.onClick.AddListener(controller.ContinueGame);
        newGameButton.onClick.AddListener(controller.NewGame);
        settingsButton.onClick.AddListener(controller.OpenSettings);
        exitButton.onClick.AddListener(controller.ExitGame);

        settingsBackButton.onClick.AddListener(controller.CloseSettings);
    }
}
