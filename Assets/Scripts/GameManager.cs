using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of GameManager
    public GameObject gameOverUI; // Reference to the GameOver UI panel

    private void Awake()
    {
        // Ensure a single instance of GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        // Pause the game
        Time.timeScale = 0;
        // Activate the GameOver UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOver UI is not assigned in the GameManager!");
        }
    }

    public void RestartGame()
    {
        // Resume the game
        Time.timeScale = 1;
        // Deactivate the GameOver UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}