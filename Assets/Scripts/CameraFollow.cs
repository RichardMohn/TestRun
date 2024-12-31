using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player's Transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset; // Offset from the player

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player’s Y-axis only, keeping the ground stable
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}