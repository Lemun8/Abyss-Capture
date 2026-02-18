using UnityEngine;

public class PhotoModeController : MonoBehaviour
{
    public GameObject photoFrame;
    private bool photoModeActive;

    public AudioSource photoIn;
    public AudioSource photoOut;

    void Update()
    {
        // 🚫 Block photo mode while paused
        if (PauseManagerExistsAndPaused())
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnterPhotoMode();
        }

        if (photoModeActive && Input.GetMouseButtonDown(1))
        {
            ExitPhotoMode();
        }
    }

    bool PauseManagerExistsAndPaused()
    {
        return PauseManagerInstance() && PauseManagerInstance().IsPaused();
    }

    PauseManager PauseManagerInstance()
    {
        return FindObjectOfType<PauseManager>();
    }

    void EnterPhotoMode()
    {
        photoModeActive = true;
        photoFrame.SetActive(true);
        photoIn.Play();
    }

    public void ExitPhotoMode()
    {
        photoModeActive = false;
        photoFrame.SetActive(false);
        photoOut.Play();
    }

    public bool IsPhotoModeActive()
    {
        return photoModeActive;
    }
}
