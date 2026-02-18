using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CreatureAI : MonoBehaviour
{
    enum State
    {
        Idle,
        Wander,
        Flee
    }

    [Header("Movement")]
    public float swimSpeed = 2f;
    public float fleeSpeed = 4f;
    public float directionChangeInterval = 3f;

    [Header("Awareness")]
    public float detectionRadius = 4f;
    public float fleeRadius = 2f;

    [SerializeField] float rotationSpeed = 8f;

    SpriteRenderer sprite;

    Rigidbody2D rb;
    Transform player;

    Vector2 wanderDirection;
    float directionTimer;
    State currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Start()
    {
        currentState = State.Wander;
        PickNewDirection();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= fleeRadius)
            currentState = State.Flee;
        else
            currentState = State.Wander;

        if (Vector2.Distance(transform.position, player.position) > 30f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;
            case State.Flee:
                Flee();
                break;
        }
    }

    void Wander()
    {
        directionTimer -= Time.fixedDeltaTime;

        if (directionTimer <= 0f)
            PickNewDirection();

        rb.AddForce(wanderDirection * swimSpeed);
        ClampSpeed(swimSpeed);
        RotateToVelocity();
    }

    void Flee()
    {
        Vector2 fleeDir = (transform.position - player.position).normalized;

        rb.AddForce(fleeDir * fleeSpeed);
        ClampSpeed(fleeSpeed);
        RotateToVelocity();
    }

    void PickNewDirection()
    {
        wanderDirection = Random.insideUnitCircle.normalized;
        directionTimer = directionChangeInterval;
    }

    void ClampSpeed(float maxSpeed)
    {
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void RotateToVelocity()
    {
        if (rb.velocity.sqrMagnitude < 0.05f)
            return;
        
        if (sprite != null)
            sprite.flipX = rb.velocity.x > 0;

        Vector2 dir = rb.velocity.normalized;

        // Prevent upside-down flipping
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Clamp vertical tilt for fish-like motion
        angle = Mathf.Clamp(angle, -45f, 45f);

        float smoothAngle = Mathf.LerpAngle(rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(smoothAngle);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fleeRadius);
    }
}
