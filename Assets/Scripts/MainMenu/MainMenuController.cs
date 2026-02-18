using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Scenes")]
    public string hubSceneName = "HubMenu";

    [Header("Settings UI")]
    public GameObject settingsPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
    }

    // ---------------- BUTTON ACTIONS ----------------

    public void ContinueGame()
    {
        SceneManager.LoadScene(hubSceneName);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene(hubSceneName);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
