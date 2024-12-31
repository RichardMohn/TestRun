using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI text
    public int score = 0; // Player's current score

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}