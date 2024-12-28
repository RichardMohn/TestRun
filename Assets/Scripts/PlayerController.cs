
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dashForce = 500f;
    public int maxSwipes = 10;
    private int currentSwipes;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSwipes = maxSwipes;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && currentSwipes > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDirection = touch.deltaPosition.normalized;
                Dash(swipeDirection);
                currentSwipes--;
            }
        }
    }

    private void Dash(Vector2 direction)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * dashForce);
    }
}
