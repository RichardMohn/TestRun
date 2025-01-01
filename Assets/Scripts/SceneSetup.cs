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

        // Ensure necessary scripts exist
        EnsureScriptsExist();

        // Remove duplicate cameras and GameObjects
        RemoveDuplicateCameras();
        RemoveDuplicateGameObjects();

        // Setup the camera
        SetupCamera();

        // Validate and attach scripts
        ValidateAndAttachScripts();

        // Create Main Menu
        CreateMainMenu();
    }

    void EnsureScriptsExist()
    {
        EnsureScriptExists("GameController", GetGameControllerScriptContent());
        EnsureScriptExists("PlayerController", GetPlayerControllerScriptContent());
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
        // Validate BlockManager
        FindOrCreateGameObject("BlockManager", obj => obj.AddComponent<BlockManager>());

        // Validate GameController
        FindOrCreateGameObject("GameController", obj =>
        {
            if (!obj.TryGetComponent<GameController>(out GameController controller))
            {
                controller = obj.AddComponent<GameController>();
            }
            Debug.Log("GameController script validated and attached to GameController GameObject.");
        });

        // Validate PlayerController
        FindOrCreateGameObject("Player", obj =>
        {
            if (!obj.TryGetComponent<PlayerController>(out PlayerController controller))
            {
                controller = obj.AddComponent<PlayerController>();
            }
            Debug.Log("PlayerController script validated and attached to Player GameObject.");
        });

        // Validate ScoreManager
        FindOrCreateGameObject("ScoreManager", obj => obj.AddComponent<ScoreManager>());

        // Validate BlockMovement
        FindOrCreateGameObject("Blocks", obj => obj.AddComponent<BlockMovement>());

        // Validate RetryButton
        FindOrCreateGameObject("RetryButton", obj => obj.AddComponent<GameOverTrigger>());
    }

    GameObject FindOrCreateGameObject(string name, System.Action<GameObject> setupAction)
    {
        GameObject obj = GameObject.Find(name);
        if (obj == null)
        {
            obj = new GameObject(name);
            setupAction?.Invoke(obj);
            Debug.Log($"Created GameObject: {name}");
        }
        else
        {
            Debug.Log($"GameObject {name} already exists.");
        }
        return obj;
    }

    void CreateMainMenu()
    {
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

        // Game Title
        CreateText(mainMenuCanvas.transform, "GameTitle", "Ball Block", new Vector2(0, 450), 80, (TMPro.FontStyles)FontStyle.Bold, Color.white);

        // Buttons
        GameObject singlePlayerButton = CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 200));
        GameObject multiplayerButton = CreateButton(mainMenuCanvas.transform, "MultiplayerButton", "Multiplayer", new Vector2(0, 100));
        GameObject settingsButton = CreateButton(mainMenuCanvas.transform, "SettingsButton", "Settings", new Vector2(0, 0));
        GameObject helpCenterButton = CreateButton(mainMenuCanvas.transform, "HelpCenterButton", "Help Center", new Vector2(0, -100));

        // Wallet Display
        CreateText(mainMenuCanvas.transform, "WalletDisplay", "Wallet: $50.00", new Vector2(-850, -450), 24, (TMPro.FontStyles)FontStyle.Normal, Color.white);

        // Player Profile Icon
        GameObject profileIcon = CreateButton(mainMenuCanvas.transform, "ProfileIcon", "Profile", new Vector2(-950, -450));
        profileIcon.GetComponent<Button>().onClick.AddListener(CreatePlayerProfileMenu);

        // Button functionality
        singlePlayerButton.GetComponent<Button>().onClick.AddListener(CreateSinglePlayerMenu);
        multiplayerButton.GetComponent<Button>().onClick.AddListener(() => LoadScene("MultiplayerScene"));
        settingsButton.GetComponent<Button>().onClick.AddListener(CreateSettingsMenu);
        helpCenterButton.GetComponent<Button>().onClick.AddListener(CreateHelpCenterMenu);

        AddBackground(mainMenuCanvas, new Color(0.1f, 0.1f, 0.2f));
    }

    GameObject CreateCanvas(string name)
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

    GameObject CreateButton(Transform parent, string name, string buttonText, Vector2 position)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent);

        Button button = buttonObject.AddComponent<Button>();
        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.2f, 0.6f, 0.8f);

        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 80);
        rectTransform.anchoredPosition = position;

        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform);

        TextMeshProUGUI buttonTextComponent = textObject.AddComponent<TextMeshProUGUI>();
        buttonTextComponent.text = buttonText;
        buttonTextComponent.fontSize = 24;
        buttonTextComponent.alignment = TextAlignmentOptions.Center;
        buttonTextComponent.color = Color.white;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(300, 80);
        textRect.anchoredPosition = Vector2.zero;

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
        text.color = color;

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 100);
        rectTransform.anchoredPosition = position;
    }

    void AddBackground(GameObject parent, Color color)
    {
        GameObject background = new GameObject("Background");
        background.transform.SetParent(parent.transform);

        Image image = background.AddComponent<Image>();
        image.color = color;

        RectTransform rectTransform = background.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1920, 1080);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    void CreatePlayerProfileMenu()
    {
        GameObject profileCanvas = CreateCanvas("PlayerProfileCanvas");

        // Wallet Info
        CreateText(profileCanvas.transform, "WalletInfo", "Wallet: $50.00", new Vector2(0, 300), 40, (TMPro.FontStyles)FontStyle.Normal, Color.white);

        // Manage Wallet Button
        GameObject manageWalletButton = CreateButton(profileCanvas.transform, "ManageWalletButton", "Manage Wallet", new Vector2(0, 150));
        manageWalletButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Manage Wallet Placeholder");
        });

        // Achievements Button
        GameObject achievementsButton = CreateButton(profileCanvas.transform, "AchievementsButton", "Achievements", new Vector2(0, 50));
        achievementsButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Achievements Placeholder");
        });

        // Back to Main Menu Button
        GameObject backButton = CreateButton(profileCanvas.transform, "BackButton", "Back to Main Menu", new Vector2(0, -150));
        backButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(profileCanvas);
            CreateMainMenu();
        });

        AddBackground(profileCanvas, new Color(0.15f, 0.15f, 0.15f));
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}