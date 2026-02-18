using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }

    public int FinalScore { get; private set; }

    [Header("Passive Score")]
    public int scorePerSecond = 1;

    bool scoringActive = true;
    float passiveScoreTimer;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (!scoringActive) return;

        // 🚫 Automatically stops when Time.timeScale = 0
        passiveScoreTimer += Time.deltaTime;

        if (passiveScoreTimer >= 1f)
        {
            int seconds = Mathf.FloorToInt(passiveScoreTimer);
            CurrentScore += seconds * scorePerSecond;
            passiveScoreTimer -= seconds;
        }
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
    }

    public void StopScoring()
    {
        scoringActive = false;
        FinalScore = CurrentScore;
    }
}
