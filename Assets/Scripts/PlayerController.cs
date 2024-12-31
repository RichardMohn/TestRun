using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}