using UnityEngine;
using Cinemachine;

public class CameraUpgradeController : MonoBehaviour
{
    [Header("References")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("Zoom Settings")]
    public float baseOrthoSize = 5f;
    public float zoomOutPerLevel = 1.2f;
    public float maxOrthoSize = 12f;

    void Start()
    {
        ApplyZoom();
    }

    public void ApplyZoom()
    {
        int cameraLevel = UpgradeManager.Instance.cameraLevel;

        float targetSize = baseOrthoSize + (cameraLevel * zoomOutPerLevel);
        targetSize = Mathf.Min(targetSize, maxOrthoSize);

        virtualCamera.m_Lens.OrthographicSize = targetSize;
    }

    void OnEnable()
    {
        UpgradeManager.OnUpgradeChanged += OnUpgrade;
    }

    void OnDisable()
    {
        UpgradeManager.OnUpgradeChanged -= OnUpgrade;
    }

    void OnUpgrade(UpgradeType type)
    {
        if (type == UpgradeType.Camera)
            ApplyZoom();
    }
}
