using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    public LayerMask creatureLayer;

    private RectTransform photoFrame;
    private Camera mainCamera;
    
    public AudioSource photoClick;

    void Start()
    {
        mainCamera = Camera.main;

        photoFrame = PhotoUIManager.Instance.photoFrame;

        if (photoFrame == null)
        {
            Debug.LogError("PhotoFrame not found!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CapturePhoto();
        }
    }

    void CapturePhoto()
    {
        if (photoFrame == null) return;

        Vector3[] corners = new Vector3[4];
        photoFrame.GetWorldCorners(corners);

        Vector2 bottomLeft = mainCamera.ScreenToWorldPoint(corners[0]);
        Vector2 topRight   = mainCamera.ScreenToWorldPoint(corners[2]);

        Vector2 center = (bottomLeft + topRight) / 2f;
        Vector2 size = new Vector2(
            Mathf.Abs(topRight.x - bottomLeft.x),
            Mathf.Abs(topRight.y - bottomLeft.y)
        );

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            center,
            size,
            0f,
            creatureLayer
        );

        foreach (Collider2D hit in hits)
        {
            Creature creature = hit.GetComponent<Creature>();
            if (creature != null)
            {
                creature.OnPhotographed(mainCamera, photoFrame);
                photoClick.Play();
            }
        }
    }
}
