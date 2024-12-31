using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player hits the ground
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the GameOver method from GameManager
            GameManager.instance.GameOver();
        }
    }
}