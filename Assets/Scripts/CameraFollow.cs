using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player's Transform
    public float smoothSpeed = 0.2f; // Smoothness of the camera movement
    public Vector3 offset; // Offset for positioning the camera
    public float stopFollowingYThreshold = -5f; // Y position where the camera stops following

    private bool stopFollowing = false;

    void LateUpdate()
    {
        if (player != null && !stopFollowing)
        {
            // Check if the player has fallen below the threshold
            if (player.position.y < stopFollowingYThreshold)
            {
                stopFollowing = true;
                return;
            }

            // Smoothly follow the player on the Y-axis only
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}