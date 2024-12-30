using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public GameObject gameOverUI;

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        lives = 3;
        gameOverUI.SetActive(false);
        // Add scene reload or reset logic here
    }
}
