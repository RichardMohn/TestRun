using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Reference to the High Score Text
    public TextMeshProUGUI currentScoreText; // Reference to the Current Score Text
    public int currentScore = 0; // The player's current score

    void Start()
    {
        // Display the high score at the start
        UpdateHighScoreText();
    }

    public void AddScore(int amount)
    {
        // Update the current score
        currentScore += amount;
        UpdateCurrentScoreText();
    }

    public void GameOver()
    {
        // Check and update the high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScore = currentScore;
        }

        // Update the high score text on the GameOver screen
        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    private void UpdateCurrentScoreText()
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = "Score: " + currentScore.ToString();
        }
    }
}