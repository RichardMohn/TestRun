using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public GameObject startScreenCanvas; // Reference to the Start Screen Canvas

    public void StartGame()
    {
        // Hide the Start Screen
        if (startScreenCanvas != null)
        {
            startScreenCanvas.SetActive(false);
        }

        // Resume the game
        Time.timeScale = 1;
    }
}