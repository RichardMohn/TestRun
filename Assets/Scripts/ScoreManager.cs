
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score;

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
