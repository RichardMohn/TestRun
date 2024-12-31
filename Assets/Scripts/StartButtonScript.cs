using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public GameObject startScreenCanvas; // Reference to the Start Screen Canvas

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0; // Pauses the game, freezing all movement
    }

    public void StartGame()
    {
        // Hide the Start Screen
        if (startScreenCanvas != null)
        {
            startScreenCanvas.SetActive(false); // Deactivates the canvas
        }

        // Resume the game
        Time.timeScale = 1; // Resumes the game
    }
}