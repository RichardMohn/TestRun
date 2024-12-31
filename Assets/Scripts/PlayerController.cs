using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f; // Force applied when the player jumps
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detect touch input on Android or mouse input in the editor
        if (Input.GetMouseButtonDown(0)) // Works for touch and mouse
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply upward force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}