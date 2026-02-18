using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;

    [Header("Buttons")]
    public Button[] toggleButtons;

    void Awake()
    {
        foreach (Button btn in toggleButtons)
        {
            btn.onClick.AddListener(ToggleTutorial);
        }
    }

    public void ToggleTutorial()
    {
        bool isOpen = tutorialPanel.activeSelf;
        tutorialPanel.SetActive(!isOpen);
    }
}
