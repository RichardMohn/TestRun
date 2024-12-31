using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float smoothSpeed = 0.2f; // Smoothness of the camera movement
    public Vector3 offset; // Offset to position the camera relative to the player
    public float startFollowingYThreshold = 2f; // Height at which the camera starts to follow
    public float stopFollowingTime = 1.5f; // Time in seconds before the camera stops following during a fall

    private bool isFollowing = false; // Tracks if the camera is actively following the player
    private float fallTimer = 0f; // Timer to track how long the player has been falling

    void LateUpdate()
    {
        if (player == null) return;

        // Start following the player once they reach the threshold
        if (!isFollowing && player.position.y > startFollowingYThreshold)
        {
            isFollowing = true;
        }

        // If the player falls below the current camera position, start the fall timer
        if (isFollowing && player.position.y < transform.position.y)
        {
            fallTimer += Time.deltaTime;

            // Stop following if the fall timer exceeds the set limit
            if (fallTimer >= stopFollowingTime)
            {
                isFollowing = false;
                return;
            }
        }
        else
        {
            // Reset the fall timer if the player is moving upwards
            fallTimer = 0f;
        }

        // Smoothly follow the player if active
        if (isFollowing)
        {
            Vector3 desiredPosition = new Vector3(
                transform.position.x, 
                player.position.y + offset.y, 
                transform.position.z
            );
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}