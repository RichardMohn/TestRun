using UnityEngine;
using UnityEngine.UI;

public class SceneSetup : MonoBehaviour
{
    public GameObject blockPrefab; // Assign the Block Prefab in the Inspector

    // Automatically run the setup when the game starts
    void Start()
    {
        SetupGameObjects();
    }

    [ContextMenu("Setup Scene")]
    public void SetupGameObjects()
    {
        // Clear the scene
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name != "Main Camera")
            {
                DestroyImmediate(obj);
            }
        }

        // Create Ground
        GameObject ground = new GameObject("Ground");
        ground.AddComponent<SpriteRenderer>().color = Color.green;
        ground.transform.localScale = new Vector3(10, 1, 1);
        ground.transform.position = new Vector3(0, -4, 0);
        ground.AddComponent<BoxCollider2D>();

        // Create Player
        GameObject player = new GameObject("Player");
        SpriteRenderer playerRenderer = player.AddComponent<SpriteRenderer>();
        playerRenderer.color = Color.red; // Assign a red color for visibility
        player.transform.localScale = new Vector3(1, 1, 1);
        player.transform.position = new Vector3(0, -3, 0);

        // Add Player Physics Components
        player.AddComponent<CircleCollider2D>();
        Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
        rb.gravityScale = 2f; // Adjust gravity for realistic jumping

        // Add Player Movement Script
        PlayerController playerController = player.AddComponent<PlayerController>();
        playerController.jumpForce = 5f;

        // Create Block Manager
        GameObject blockManager = new GameObject("BlockManager");
        BlockManager blockManagerScript = blockManager.AddComponent<BlockManager>();
        blockManagerScript.blockPrefab = blockPrefab; // Assign the Block Prefab dynamically
        blockManagerScript.player = player.transform; // Reference the player for spawning logic

        // Create UI Canvas
        GameObject canvas = new GameObject("Canvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        canvas.AddComponent<GraphicRaycaster>();

        // Create Score Text
        GameObject scoreText = new GameObject("ScoreText");
        scoreText.transform.SetParent(canvas.transform);
        Text scoreTextComp = scoreText.AddComponent<Text>();
        scoreTextComp.text = "Score: 0";
        scoreTextComp.fontSize = 36;
        scoreTextComp.alignment = TextAnchor.MiddleCenter;
        RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
        scoreRect.sizeDelta = new Vector2(400, 100);
        scoreRect.anchoredPosition = new Vector2(0, 800);

        // Create Game Over Screen
        GameObject gameOverPanel = new GameObject("GameOverPanel");
        gameOverPanel.transform.SetParent(canvas.transform);
        Image panelImage = gameOverPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.5f); // Semi-transparent black
        RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(1080, 1920);
        panelRect.anchoredPosition = Vector2.zero;

        // Add Retry Button
        GameObject retryButton = new GameObject("RetryButton");
        retryButton.transform.SetParent(gameOverPanel.transform);
        Button buttonComp = retryButton.AddComponent<Button>();
        Image buttonImage = retryButton.AddComponent<Image>();
        buttonImage.color = Color.white; // White button
        RectTransform buttonRect = retryButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(300, 100);
        buttonRect.anchoredPosition = new Vector2(0, -100);

        // Add Text to Retry Button
        GameObject retryText = new GameObject("RetryText");
        retryText.transform.SetParent(retryButton.transform);
        Text retryTextComp = retryText.AddComponent<Text>();
        retryTextComp.text = "Retry";
        retryTextComp.fontSize = 36;
        retryTextComp.alignment = TextAnchor.MiddleCenter;
        retryTextComp.color = Color.black;
        RectTransform retryTextRect = retryText.GetComponent<RectTransform>();
        retryTextRect.sizeDelta = new Vector2(300, 100);
        retryTextRect.anchoredPosition = Vector2.zero;

        // Initially hide the Game Over panel
        gameOverPanel.SetActive(false);

        Debug.Log("Scene setup complete!");
    }
}