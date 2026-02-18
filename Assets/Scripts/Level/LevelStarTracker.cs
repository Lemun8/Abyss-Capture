using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelStarTracker : MonoBehaviour
{
    public System.Action OnStarsChanged;
    public LevelData levelData;

    HashSet<string> documentedCreatures = new();
    int starsAwarded;

    int totalSpecies;

    void Start()
    {
        totalSpecies = GetTotalUniqueSpecies();
        starsAwarded = LevelStarSave.GetStars(levelData.levelID);
    }

    int GetTotalUniqueSpecies()
    {
        return levelData.spawnTable.creatures
            .Select(c => c.creatureData.creatureID)
            .Distinct()
            .Count();
    }

    public void RegisterCreatureDocumented(string creatureID)
    {
        if (documentedCreatures.Contains(creatureID))
            return;

        documentedCreatures.Add(creatureID);

        EvaluateStars();
    }

    void EvaluateStars()
    {
        int count = documentedCreatures.Count;
        int newStars = CalculateStars(count);

        if (newStars > starsAwarded)
        {
            int starsGained = newStars - starsAwarded;
            starsAwarded = newStars;

            LevelStarSave.SaveStars(levelData.levelID, starsAwarded);

            int reward = starsGained * levelData.creditRewardPerStar;
            CurrencyManager.Instance.AddCurrency(reward);

            OnStarsChanged?.Invoke();

            Debug.Log($"Level Stars: {starsAwarded}/{GetMaxStars()} | +{reward} credits");
        }
    }

    int CalculateStars(int documentedCount)
    {
        if (documentedCount >= totalSpecies)
            return 3;
        if (documentedCount >= Mathf.CeilToInt(totalSpecies * 0.6f))
            return 2;
        if (documentedCount >= 1)
            return 1;

        return 0;
    }

    public int GetMaxStars() => 3;
    public int GetCurrentStars() => starsAwarded;
}
