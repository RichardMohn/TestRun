using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float offset = 2f; // Offset to keep the player near the center

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = player.position.y + offset;
            transform.position = newPosition;
        }
    }
}