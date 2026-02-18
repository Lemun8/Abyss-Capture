using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    [Header("UI")]
    public GameObject settingsPanel;

    [Header("Buttons")]
    public Button[] toggleButtons;
    public Button backToMenuButton;

    public string SceneName = "MainMenu";

    void Awake()
    {
        foreach (Button btn in toggleButtons)
        {
            btn.onClick.AddListener(ToggleSettings);
        }

        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void ToggleSettings()
    {
        bool isOpen = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isOpen);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneName);
    }
}
