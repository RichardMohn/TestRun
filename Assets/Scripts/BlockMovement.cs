using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float moveRange = 2f; // Distance the block moves from its starting position

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        // Save the starting position of the block
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the block back and forth
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + moveRange)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= startPosition.x - moveRange)
                movingRight = true;
        }
    }
}