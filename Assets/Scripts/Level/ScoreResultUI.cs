using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreResultUI : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text scoreText;
    public TMP_Text currencyText;
    public Button returnButton;

    [Header("Stars")]
    public Image[] starImages;
    public Sprite starFilled;
    public Sprite starEmpty;
    public LevelStarTracker starTracker;

    public GameObject resultShow;

    bool rewardClaimed;

    void Awake()
    {
        panel.SetActive(false);
        returnButton.onClick.AddListener(ReturnToMenu);
    }

    void OnEnable()
    {
        PlayerOxygen.OnOxygenDepleted += Show;
    }

    void OnDisable()
    {
        PlayerOxygen.OnOxygenDepleted -= Show;
    }

    void Show()
    {
        panel.SetActive(true);
        resultShow.SetActive(true);
        
        int finalScore = ScoreManager.Instance.FinalScore;
        scoreText.text = $"Score: {finalScore}";
        currencyText.text = $"Credits Earned: {finalScore}";

        int stars = starTracker.GetCurrentStars();
        

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = i < stars ? starFilled : starEmpty;
        }
    }

    void ReturnToMenu()
    {
        if (rewardClaimed)
            return;

        rewardClaimed = true;

        int reward = ScoreManager.Instance.FinalScore;
        CurrencyManager.Instance.AddCurrency(reward);

        returnButton.interactable = false;

        SceneManager.LoadScene("Hub Menu");
    }
}
