using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
