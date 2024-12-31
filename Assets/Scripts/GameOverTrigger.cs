using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject gameOverCanvas; // Assign the GameOverCanvas in the Inspector
    public Transform player; // Reference to the Player's Transform

    private float cameraBottomY;

    void Start()
    {
        // Ensure the Game Over Canvas is hidden at the start
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Calculate the bottom of the camera
        cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;

        // Check if the player has fallen below the screen
        if (player.position.y < cameraBottomY)
        {
            TriggerGameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player hits the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        // Show the Game Over Canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        // Restart the game by reloading the current scene
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}