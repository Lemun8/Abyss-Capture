using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        if (ScoreManager.Instance == null)
            return;

        scoreText.text = $"Score: {ScoreManager.Instance.CurrentScore}";
    }
}
