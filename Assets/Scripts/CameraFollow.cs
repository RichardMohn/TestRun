using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player's Transform
    public float smoothSpeed = 0.2f; // Smoothness of the camera movement
    public Vector3 offset; // Offset from the player

    private float initialCameraY; // Keeps the camera's starting Y position

    void Start()
    {
        // Store the initial Y position of the camera
        initialCameraY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Only follow the player when they move up
            if (player.position.y > initialCameraY)
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}