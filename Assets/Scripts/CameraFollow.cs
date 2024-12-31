using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float smoothSpeed = 0.2f; // Smoothness of the camera movement
    public Vector3 offset; // Offset for positioning the camera
    public float stopFollowingYThreshold = -5f; // Y position where the camera stops following

    private float initialCameraY; // The starting Y position of the camera
    private bool stopFollowing = false; // Tracks whether to stop following the player

    void Start()
    {
        // Store the initial Y position of the camera
        initialCameraY = transform.position.y;
    }

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

            // Smoothly follow the player, only moving the camera upward
            float targetY = Mathf.Max(initialCameraY, player.position.y + offset.y);
            Vector3 desiredPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}