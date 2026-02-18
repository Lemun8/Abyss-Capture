using UnityEngine;

public static class LevelStarSave
{
    static string Key(string levelID) => $"LEVEL_STARS_{levelID}";

    public static int GetStars(string levelID)
    {
        return PlayerPrefs.GetInt(Key(levelID), 0);
    }

    public static void SaveStars(string levelID, int stars)
    {
        int current = GetStars(levelID);
        if (stars > current)
        {
            PlayerPrefs.SetInt(Key(levelID), stars);
            PlayerPrefs.Save();
        }
    }
}
