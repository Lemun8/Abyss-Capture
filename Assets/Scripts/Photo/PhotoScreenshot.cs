using UnityEngine;
using System.IO;

public static class PhotoScreenshot
{
    public static string Capture(
        Camera cam,
        RectTransform frame,
        string creatureID
    )
    {
        // Convert UI frame to screen space
        Vector3[] corners = new Vector3[4];
        frame.GetWorldCorners(corners);

        int x = Mathf.Clamp(Mathf.RoundToInt(corners[0].x), 0, Screen.width);
        int y = Mathf.Clamp(Mathf.RoundToInt(corners[0].y), 0, Screen.height);

        int width = Mathf.Clamp(
            Mathf.RoundToInt(corners[2].x - corners[0].x),
            1,
            Screen.width - x
        );

        int height = Mathf.Clamp(
            Mathf.RoundToInt(corners[2].y - corners[0].y),
            1,
            Screen.height - y
        );

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = rt;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        cam.Render();
        RenderTexture.active = rt;

        tex.ReadPixels(new Rect(x, y, width, height), 0, 0);
        tex.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;
        Object.Destroy(rt);

        byte[] png = tex.EncodeToPNG();
        Object.Destroy(tex);

        string dir = Path.Combine(
            Application.persistentDataPath,
            "EncyclopediaPhotos"
        );

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string path = Path.Combine(dir, creatureID + ".png");
        File.WriteAllBytes(path, png);

        return path;
    }
}
