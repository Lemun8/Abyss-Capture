using UnityEngine;
using UnityEngine.UI;

public class LevelStarUI : MonoBehaviour
{
    [Header("References")]
    public LevelStarTracker starTracker;

    [Header("Star UI")]
    public Image[] starImages;
    public Sprite starFilled;
    public Sprite starEmpty;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        int stars = starTracker.GetCurrentStars();

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite =
                i < stars ? starFilled : starEmpty;
        }
    }

    void OnEnable()
    {
        starTracker.OnStarsChanged += Refresh;
    }

    void OnDisable()
    {
        starTracker.OnStarsChanged -= Refresh;
    }
}
