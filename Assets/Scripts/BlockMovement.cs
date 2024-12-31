using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float baseSpeed = 1f; // Base speed of the block
    public float speedIncrease = 0.1f; // Speed increase after each block is landed on
    public float range = 2f; // Horizontal range for movement
    private float startX;
    private float currentSpeed;

    void Start()
    {
        // Store the initial X position and set the starting speed
        startX = transform.position.x;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Move the block back and forth using a sine wave
        float newX = startX + Mathf.Sin(Time.time * currentSpeed) * range;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    public void IncreaseSpeed()
    {
        // Increase the block's speed
        currentSpeed += speedIncrease;
    }
}