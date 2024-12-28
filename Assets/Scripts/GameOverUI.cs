
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameController gameController;

    public void OnRestartButtonPressed()
    {
        gameController.RestartGame();
    }
}
