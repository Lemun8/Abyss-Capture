using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class UIButtonFX : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    [Header("Animation")]
    public float hoverScale = 1.1f;
    public float tweenDuration = 0.15f;
    public Ease ease = Ease.OutBack;

    Vector3 originalScale;
    Tween scaleTween;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale * hoverScale, tweenDuration)
            .SetEase(ease);

        UISFXManager.Instance?.PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        scaleTween?.Kill();
        scaleTween = transform
            .DOScale(originalScale, tweenDuration)
            .SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UISFXManager.Instance?.PlayClick();

        // Optional punch for feedback
        transform.DOPunchScale(Vector3.one * 0.1f, 0.15f, 8, 1f);
    }

    void OnDisable()
    {
        scaleTween?.Kill();
        transform.localScale = originalScale;
    }
}
