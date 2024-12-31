using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player's Transform
    public float smoothSpeed = 0.2f; // Smoothness of the camera movement
    public Vector3 offset; // Offset to position the player correctly in view
    public float delayThreshold = 2f; // Distance before the camera starts to follow

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
            // Only follow the player if they are above the delay threshold
            float cameraTargetY = Mathf.Max(initialCameraY, player.position.y - delayThreshold);

            // Calculate the desired position
            Vector3 desiredPosition = new Vector3(transform.position.x, cameraTargetY + offset.y, transform.position.z);

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}