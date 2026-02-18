using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [Header("Setup")]
    public Transform notificationRoot;
    public GameObject notificationPrefab;

    [Header("Timing")]
    public float lifetime = 2.5f;
    public float fadeDuration = 0.3f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static void Notify(string message)
    {
        if (Instance == null)
        {
            Debug.LogWarning("NotificationManager missing!");
            return;
        }

        Instance.Show(message);
    }

    void Show(string message)
    {
        GameObject go = Instantiate(notificationPrefab, notificationRoot);

        TMP_Text text = go.GetComponentInChildren<TMP_Text>();
        CanvasGroup cg = go.GetComponent<CanvasGroup>();

        text.text = message;
        cg.alpha = 0f;

        // Animate in
        cg.DOFade(1f, fadeDuration).SetUpdate(true);
        go.transform.DOLocalMoveY(
            go.transform.localPosition.y + 20f,
            fadeDuration
        ).SetRelative().SetUpdate(true);

        // Auto remove
        DOVirtual.DelayedCall(lifetime, () =>
        {
            cg.DOFade(0f, fadeDuration)
              .SetUpdate(true)
              .OnComplete(() => Destroy(go));
        });
    }
}
