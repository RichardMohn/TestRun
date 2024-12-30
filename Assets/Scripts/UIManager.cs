using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverUI.SetActive(false);
    }
}
