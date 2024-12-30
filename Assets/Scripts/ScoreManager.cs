using UnityEngine;
using UnityEngine.UI; // Ensure this namespace is included for UI elements like Text

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the UI Text component for displaying the score
    private int score = 0;

    public void AddScore()
    {
        score++;
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the Inspector!");
        }
    }
}