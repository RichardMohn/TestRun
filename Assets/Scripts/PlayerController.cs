using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 swipeDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rb.velocity = swipeDirection.normalized * bounceForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< main
        if (collision.gameObject.CompareTag("Block"))
        {
            rb.velocity = Vector2.up * bounceForce;
        }
=======
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * dashForce);
>>>>>>> origin/main
    }
}
