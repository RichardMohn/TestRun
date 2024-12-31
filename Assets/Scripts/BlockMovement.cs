using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float speed = 1f; // Speed of the block's movement
    public float range = 2f; // Horizontal range for movement
    private float startX;

    void Start()
    {
        // Store the initial X position
        startX = transform.position.x;
    }

    void Update()
    {
        // Move the block back and forth using a sine wave
        float newX = startX + Mathf.Sin(Time.time * speed) * range;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}