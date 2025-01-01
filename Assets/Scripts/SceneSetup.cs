using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneSetup : MonoBehaviour
{
    private static bool hasRun = false;

    void Start()
    {
        if (hasRun) return;
        hasRun = true;

        EnsureScriptsExist();
        RemoveDuplicateCameras();
        RemoveDuplicateGameObjects();
        SetupCamera();
        CreateMainMenu();
        CreatePlayer();
    }

    private void EnsureScriptsExist()
    {
        EnsureScriptExists("GameController", GetGameControllerScriptContent());
        EnsureScriptExists("PlayerController", GetPlayerControllerScriptContent());
    }

    private void EnsureScriptExists(string scriptName, string scriptContent)
    {
        string path = Application.dataPath + $"/Scripts/{scriptName}.cs";
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, scriptContent);
            Debug.Log($"{scriptName} script created at {path}");
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }

    private string GetGameControllerScriptContent()
    {
        return @"
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }

    public void ResetGame()
    {
        Score = 0;
    }
}
";
    }

    private string GetPlayerControllerScriptContent()
    {
        return @"
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }
}
";
    }

    private void RemoveDuplicateCameras()
    {
        Camera[] cameras = Object.FindObjectsByType<Camera>(FindObjectsSortMode.None);
        if (cameras.Length > 1)
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                Destroy(cameras[i].gameObject);
            }
            Debug.Log("Duplicate cameras removed.");
        }
    }

    private void RemoveDuplicateGameObjects()
    {
        string[] objectsToCheck = { "MainMenuCanvas", "GameController", "PlayerController" };

        foreach (string name in objectsToCheck)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"Duplicate {name} removed.");
            }
        }
    }

    private void SetupCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            mainCamera = cameraObject.AddComponent<Camera>();
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 5;
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = Color.black;
        }
        Debug.Log("Camera setup complete.");
    }

    private void CreateMainMenu()
    {
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

        CreateText(mainMenuCanvas.transform, "GameTitle", "Ball Block", new Vector2(0, 450), 80, TMPro.FontStyles.Bold, Color.white);
        CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 200));
    }

    private GameObject CreateCanvas(string name)
    {
        GameObject canvasObject = new GameObject(name);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        canvasObject.AddComponent<GraphicRaycaster>();
        return canvasObject;
    }

    private GameObject CreateButton(Transform parent, string name, string buttonText, Vector2 position)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent);

        RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 80);
        rectTransform.anchoredPosition = position;

        Button button = buttonObject.AddComponent<Button>();
        buttonObject.AddComponent<Image>();

        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform);
        TextMeshProUGUI buttonTextComponent = textObject.AddComponent<TextMeshProUGUI>();
        buttonTextComponent.text = buttonText;
        buttonTextComponent.fontSize = 24;
        buttonTextComponent.alignment = TextAlignmentOptions.Center;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(300, 80);
        textRect.anchoredPosition = Vector2.zero;

        return buttonObject;
    }

    private void CreateText(Transform parent, string name, string content, Vector2 position, int fontSize, TMPro.FontStyles fontStyle, Color color)
    {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(parent);
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 100);
        rectTransform.anchoredPosition = position;
    }

    private void CreatePlayer()
    {
        GameObject player = new GameObject("Player");
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<CircleCollider2D>();
        player.AddComponent<PlayerController>();
    }
}