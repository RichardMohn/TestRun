using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    public ScoreManager scoreManager; // Reference to the ScoreManager
    public Transform player; // Reference to the player

    private bool isPassed = false; // Tracks whether the block has been passed

    void Update()
    {
        // Check if the player has passed this block
        if (!isPassed && player.position.y > transform.position.y)
        {
            isPassed = true; // Mark the block as passed
            if (scoreManager != null)
            {
                scoreManager.AddScore(1); // Increment the score
            }
        }
    }
}