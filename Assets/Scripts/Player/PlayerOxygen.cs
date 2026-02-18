using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    [Header("Oxygen")]
    public float maxOxygen = 100f;
    public float oxygenDrainIdle = 1f;
    public float oxygenDrainMoving = 2.5f;
    public float oxygenDrainPhotoMode = 4f;

    public float CurrentOxygen { get; private set; }

    PlayerUnderwaterMovement movement;
    PhotoModeController photoMode;

    public static System.Action<PlayerOxygen> OnPlayerOxygenSpawned;
    public static System.Action OnOxygenDepleted;

    void Awake()
    {
        CurrentOxygen = maxOxygen;
        movement = GetComponent<PlayerUnderwaterMovement>();
    }

    void Start()
    {
        photoMode = FindObjectOfType<PhotoModeController>();
        OnPlayerOxygenSpawned?.Invoke(this);
        int level = UpgradeManager.Instance.oxygenLevel;
        UpgradeData oxygenUpgrade = UpgradeDatabase.Instance.GetUpgrade(UpgradeType.Oxygen);

        maxOxygen += level * oxygenUpgrade.valuePerLevel;
        CurrentOxygen = maxOxygen;
    }

    void Update()
    {
        DrainOxygen();
    }

    void DrainOxygen()
    {
        float drain = oxygenDrainIdle;

        if (movement != null && movement.enabled && movementVelocity())
            drain = oxygenDrainMoving;

        if (photoMode != null && photoMode.IsPhotoModeActive())
            drain = oxygenDrainPhotoMode;

        CurrentOxygen -= drain * Time.deltaTime;
        CurrentOxygen = Mathf.Clamp(CurrentOxygen, 0f, maxOxygen);

        if (CurrentOxygen <= 0f)
            ForceSurface();
    }

    bool movementVelocity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        return rb != null && rb.velocity.sqrMagnitude > 0.05f;
    }

    void ForceSurface()
    {
        ScoreManager.Instance.StopScoring();
        OnOxygenDepleted?.Invoke();
    }
}
