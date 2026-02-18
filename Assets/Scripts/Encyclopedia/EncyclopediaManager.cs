using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public static EncyclopediaManager Instance;

    Dictionary<string, string> photoPaths = new();

    const string COUNT_KEY = "ENCYCLOPEDIA_COUNT";
    const string ID_KEY = "ENCYCLOPEDIA_ID_";
    const string PATH_KEY = "ENCYCLOPEDIA_PATH_";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void Unlock(string creatureID, string photoPath)
    {
        if (!photoPaths.ContainsKey(creatureID))
        {
            photoPaths.Add(creatureID, photoPath);
            return;
        }

        // Upgrade existing entry with photo
        if (!string.IsNullOrEmpty(photoPath))
        {
            photoPaths[creatureID] = photoPath;
        }
        Save();
    }

    public bool IsDiscovered(string creatureID)
    {
        return photoPaths.ContainsKey(creatureID);
    }

    public Sprite GetCreatureSprite(string creatureID)
    {
        if (!photoPaths.TryGetValue(creatureID, out string path))
            return null;

        if (string.IsNullOrEmpty(path))
            return null;

        if (!System.IO.File.Exists(path))
            return null;

        byte[] data = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);

        return Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    void Save()
    {
        PlayerPrefs.SetInt(COUNT_KEY, photoPaths.Count);

        int index = 0;
        foreach (var pair in photoPaths)
        {
            PlayerPrefs.SetString(ID_KEY + index, pair.Key);
            PlayerPrefs.SetString(PATH_KEY + index, pair.Value);
            index++;
        }

        PlayerPrefs.Save();
    }

    void Load()
    {
        photoPaths.Clear();

        int count = PlayerPrefs.GetInt(COUNT_KEY, 0);

        for (int i = 0; i < count; i++)
        {
            string id = PlayerPrefs.GetString(ID_KEY + i);
            string path = PlayerPrefs.GetString(PATH_KEY + i);

            if (!string.IsNullOrEmpty(id))
                photoPaths[id] = path;
        }
    }
}
