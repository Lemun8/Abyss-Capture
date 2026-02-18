using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header("Creature Data")]
    public CreatureData data;

    bool alreadyCaptured;

    const int PHOTO_CAMERA_LEVEL = 5;

    public void OnPhotographed(
        Camera cam,
        RectTransform frame
    )
    {
        if (alreadyCaptured)
            return;

        alreadyCaptured = true;

        bool canTakePhoto =
            UpgradeManager.Instance != null &&
            UpgradeManager.Instance.cameraLevel >= PHOTO_CAMERA_LEVEL;

        string photoPath = null;

        if (canTakePhoto)
        {
            photoPath = PhotoScreenshot.Capture(
                cam,
                frame,
                data.creatureID
            );
        }

        EncyclopediaManager.Instance.Unlock(
            data.creatureID,
            photoPath
        );

        ScoreManager.Instance.AddScore(data.photoScore);

        FindObjectOfType<LevelStarTracker>()
            ?.RegisterCreatureDocumented(data.creatureID);

        NotificationManager.Notify(
            canTakePhoto
            ? $"Captured: {data.displayName} [PHOTO SAVED]"
            : $"Captured: {data.displayName} [PHOTO NOT SAVED]"
        );
    }
}
