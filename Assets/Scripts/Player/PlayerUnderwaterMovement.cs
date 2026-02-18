using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerUnderwaterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 12f;
    public float maxSpeed = 4f;

    [Header("Animation")]
    public Animator animator; // Assigned via Inspector

    Rigidbody2D rb;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Animator not assigned on PlayerUnderwaterMovement!");
        }
    }

    void Update()
    {
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (animator != null)
        {
            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetFloat("Speed", rb.velocity.magnitude);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(input * acceleration);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
