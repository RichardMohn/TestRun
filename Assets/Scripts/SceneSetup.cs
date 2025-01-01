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

        // Ensure all necessary scripts exist
        EnsureScriptsExist();

        // Remove duplicate cameras and GameObjects
        RemoveDuplicateCameras();
        RemoveDuplicateGameObjects();

        // Setup the camera
        SetupCamera();

        // Validate and attach scripts
        ValidateAndAttachScripts();

        // Create the main menu
        CreateMainMenu();
    }

    void EnsureScriptsExist()
    {
        EnsureScriptExists("GameController", GetGameControllerScriptContent());
        EnsureScriptExists("PlayerController", GetPlayerControllerScriptContent());
        EnsureScriptExists("CreateSinglePlayerMenu", GetPlaceholderScriptContent("CreateSinglePlayerMenu"));
        EnsureScriptExists("CreateSettingsMenu", GetPlaceholderScriptContent("CreateSettingsMenu"));
        EnsureScriptExists("CreateHelpMenu", GetPlaceholderScriptContent("CreateHelpMenu"));
    }

    void EnsureScriptExists(string scriptName, string scriptContent)
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

    string GetGameControllerScriptContent()
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

    string GetPlayerControllerScriptContent()
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
        if (Input.GetMouseButtonDown(0)) // Tap or click to jump
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(""Obstacle""))
        {
            GameController.Instance.ResetGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene(""GameOverScene"");
        }
    }
}
";
    }

    string GetPlaceholderScriptContent(string scriptName)
    {
        return $@"
using UnityEngine;

public class {scriptName} : MonoBehaviour
{
    public void Execute()
    {
        Debug.Log(""{scriptName} is a placeholder script. Please implement functionality as needed."");
    }
}
";
    }

    void RemoveDuplicateCameras()
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

    void RemoveDuplicateGameObjects()
    {
        string[] objectsToCheck = {
            "MainMenuCanvas", "PlayerProfileCanvas", "BlockManager", "GameController",
            "ScoreManager", "RetryButton", "GameOverScreen", "Blocks", 
            "StartScreen", "HelpCenterCanvas"
        };

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

    void SetupCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            GameObject cameraObject = new GameObject("Main Camera");
            mainCamera = cameraObject.AddComponent<Camera>();
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 5;
        }

        Debug.Log("Camera setup complete.");

        #if UNITY_EDITOR
        Screen.SetResolution(1920, 1080, false);
        #endif
    }

    void ValidateAndAttachScripts()
    {
        FindOrCreateGameObject("BlockManager", obj => obj.AddComponent<BlockManager>());
        FindOrCreateGameObject("GameController", obj => 
        {
            if (!obj.TryGetComponent<GameController>(out GameController controller))
            {
                controller = obj.AddComponent<GameController>();
            }
        });
    }

    GameObject FindOrCreateGameObject(string name, System.Action<GameObject> setupAction)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null)
        {
            obj = new GameObject(name);
            setupAction?.Invoke(obj);
        }
        return obj;
    }

    void CreateMainMenu()
    {
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

        // Game Title
        CreateText(mainMenuCanvas.transform, "GameTitle", "Ball Block", new Vector2(0, 450), 80, TMPro.FontStyles.Bold, Color.white);

        // Buttons
        CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 200));
    }

    GameObject CreateCanvas(string name)
    {
        GameObject canvasObject = new GameObject(name);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        return canvasObject;
    }

    GameObject CreateButton(Transform parent, string name, string buttonText, Vector2 position)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent);

        Button button = buttonObject.AddComponent<Button>();
        Image image = buttonObject.AddComponent<Image>();
        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 80);
        rectTransform.anchoredPosition = position;

        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform);
        TextMeshProUGUI buttonText = textObject.AddComponent<TextMeshProUGUI>();
        buttonText.text = buttonText;
        buttonText.fontSize = 24;
        buttonText.alignment = TextAlignmentOptions.Center;

        return buttonObject;
    }

    void CreateText(Transform parent, string name, string content, Vector2 position, int fontSize, TMPro.FontStyles fontStyle, Color color)
    {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(parent);
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.alignment = TextAlignmentOptions.Center;
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 100);
        rectTransform.anchoredPosition = position;
    }
}