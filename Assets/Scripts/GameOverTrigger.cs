using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject gameOverCanvas; // Reference to the Game Over Canvas
    public Transform player; // Reference to the Player Transform

    private float cameraBottomY; // Bottom of the camera's visible area
    private float cameraLeftX; // Left of the camera's visible area
    private float cameraRightX; // Right of the camera's visible area

    void Start()
    {
        // Hide the Game Over Canvas at the start
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Calculate camera boundaries
        Camera cam = Camera.main;
        cameraBottomY = cam.transform.position.y - cam.orthographicSize;
        cameraLeftX = cam.transform.position.x - cam.aspect * cam.orthographicSize;
        cameraRightX = cam.transform.position.x + cam.aspect * cam.orthographicSize;

        // Check if the player is out of bounds
        if (player.position.y < cameraBottomY || 
            player.position.x < cameraLeftX || 
            player.position.x > cameraRightX)
        {
            TriggerGameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Trigger Game Over when the player hits the ground
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
        // Reload the current scene to restart the game
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}