using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncyclopediaUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public Image playerPhotoImage;
    public Image realLifeImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    [Header("Navigation")]
    public Button nextButton;
    public Button prevButton;
    public Button closeButton;
    public Button openButton;

    public Button litButton;
    public Button twilightButton;
    public Button midnightButton;

    [Header("Creature Database (ORDER MATTERS)")] 
    public CreatureData[] allCreatures;

    [Header("Zone Filtering")]
    public DepthZone currentZone;
    public TMP_Text currentZoneText;

    List<CreatureData> filteredCreatures = new();

    int currentIndex;

    void Awake()
    {
        panel.SetActive(false);

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);
        closeButton.onClick.AddListener(Close);
        openButton.onClick.AddListener(Open);
        litButton.onClick.AddListener(SelectLitZone);
        twilightButton.onClick.AddListener(SelectTwilightZone);
        midnightButton.onClick.AddListener(SelectMidnightZone);

    }

    public void Open()
    {
        currentZone = DepthZone.Lit; // default zone
        BuildFilteredList();

        panel.SetActive(true);
        RefreshPage();
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    void RefreshPage()
    {
        if (filteredCreatures.Count == 0)
            return;

        CreatureData data = filteredCreatures[currentIndex];
        bool unlocked = EncyclopediaManager.Instance.IsDiscovered(data.creatureID);

        if (unlocked)
        {
            Sprite photo = EncyclopediaManager.Instance.GetCreatureSprite(data.creatureID);
            playerPhotoImage.sprite = photo;
            playerPhotoImage.color = photo ? Color.white : Color.clear;

            realLifeImage.sprite = data.realLifeImage;
            realLifeImage.color = Color.white;

            nameText.text = data.displayName;
            descriptionText.text = data.description;
            currentZoneText.text = $"{currentZone.ToString()} Zone";
        }
        else
        {
            playerPhotoImage.sprite = null;
            playerPhotoImage.color = Color.clear;

            realLifeImage.sprite = data.lockedPlaceholder;
            realLifeImage.color = Color.white;

            nameText.text = "???";
            descriptionText.text = data.lockedDescription;
            currentZoneText.text = $"{currentZone.ToString()} Zone";
        }

        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < filteredCreatures.Count - 1;
    }

    void BuildFilteredList()
    {
        filteredCreatures.Clear();

        foreach (var creature in allCreatures)
        {
            if (creature.depthZone == currentZone)
                filteredCreatures.Add(creature);
        }

        currentIndex = 0;
    }

    void NextPage()
    {
        if (currentIndex < filteredCreatures.Count - 1)
        {
            currentIndex++;
            RefreshPage();
        }
    }

    void PreviousPage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            RefreshPage();
        }
    }

    public void SelectLitZone()
    {
        currentZone = DepthZone.Lit;
        BuildFilteredList();
        RefreshPage();
    }

    public void SelectTwilightZone()
    {
        currentZone = DepthZone.Twilight;
        BuildFilteredList();
        RefreshPage();
    }

    public void SelectMidnightZone()
    {
        currentZone = DepthZone.Midnight;
        BuildFilteredList();
        RefreshPage();
    }
}
