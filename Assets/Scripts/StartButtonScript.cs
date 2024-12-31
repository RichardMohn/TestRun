using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public GameObject startScreenCanvas; // Reference to the Start Screen Canvas
    public GameObject player; // Reference to the Player GameObject

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0;

        // Hide the Player at the start
        if (player != null)
        {
            player.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Show the Player when the game starts
        if (player != null)
        {
            player.SetActive(true);
        }

        // Resume the game
        Time.timeScale = 1;

        // Hide the Start Screen Canvas
        if (startScreenCanvas != null)
        {
            startScreenCanvas.SetActive(false);
        }
    }
}