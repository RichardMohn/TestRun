using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    private static bool hasRun = false;

    void Start()
    {
        if (hasRun) return; // Prevent script from running multiple times
        hasRun = true;

        CreateMainMenu();
        RenameStartScreenCanvas();
    }

    private void CreateMainMenu()
    {
        // Check if MainMenuCanvas already exists
        if (GameObject.Find("MainMenuCanvas") != null) return;

        // Create Main Menu Canvas
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

        // Add a background
        AddBackground(mainMenuCanvas.transform);

        // Create Game Title
        CreateText(
            mainMenuCanvas.transform,
            "GameTitle",
            "Ball Block",
            new Vector2(0, 450),
            80,
            TMPro.FontStyles.Bold,
            Color.white
        );

        // Create Buttons
        GameObject singlePlayerButton = CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 200));
        singlePlayerButton.GetComponent<Button>().onClick.AddListener(() => LoadSinglePlayerModesScene());

        GameObject multiplayerButton = CreateButton(mainMenuCanvas.transform, "MultiplayerButton", "Multiplayer", new Vector2(0, 100));
        // Placeholder for multiplayer functionality

        GameObject settingsButton = CreateButton(mainMenuCanvas.transform, "SettingsButton", "Settings", new Vector2(0, 0));
        // Placeholder for settings functionality

        // Create Wallet Display
        CreateText(
            mainMenuCanvas.transform,
            "WalletDisplay",
            "Wallet: $0.00",
            new Vector2(800, -450), // Bottom-right corner
            24,
            TMPro.FontStyles.Normal,
            Color.green
        );
    }

    private void RenameStartScreenCanvas()
    {
        GameObject startScreenCanvas = GameObject.Find("StartScreenCanvas");
        if (startScreenCanvas != null)
        {
            startScreenCanvas.name = "SinglePlayerEndlessMode";
        }
    }

    private void LoadSinglePlayerModesScene()
    {
        // Check if the SinglePlayerModesCanvas already exists
        if (GameObject.Find("SinglePlayerModesCanvas") != null)
        {
            ShowSinglePlayerModesScene();
            return;
        }

        // Create Single Player Modes Canvas
        GameObject singlePlayerModesCanvas = CreateCanvas("SinglePlayerModesCanvas");

        // Add a background
        AddBackground(singlePlayerModesCanvas.transform);

        // Create Title
        CreateText(
            singlePlayerModesCanvas.transform,
            "SinglePlayerModesTitle",
            "Single Player Modes",
            new Vector2(0, 450),
            60,
            TMPro.FontStyles.Bold,
            Color.white
        );

        // Create Endless Mode Button
        GameObject endlessModeButton = CreateButton(singlePlayerModesCanvas.transform, "EndlessModeButton", "Endless Mode", new Vector2(0, 200));
        endlessModeButton.GetComponent<Button>().onClick.AddListener(() => LoadEndlessModeScene());

        // Hide Main Menu Canvas
        GameObject mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
    }

    private void ShowSinglePlayerModesScene()
    {
        GameObject singlePlayerModesCanvas = GameObject.Find("SinglePlayerModesCanvas");
        if (singlePlayerModesCanvas != null)
        {
            singlePlayerModesCanvas.SetActive(true);
        }

        GameObject mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false);
        }
    }

    private void LoadEndlessModeScene()
    {
        // Show Endless Mode Scene (existing SinglePlayerEndlessMode scene)
        GameObject endlessModeCanvas = GameObject.Find("SinglePlayerEndlessMode");
        if (endlessModeCanvas != null)
        {
            endlessModeCanvas.SetActive(true);
        }

        GameObject singlePlayerModesCanvas = GameObject.Find("SinglePlayerModesCanvas");
        if (singlePlayerModesCanvas != null)
        {
            singlePlayerModesCanvas.SetActive(false);
        }
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

    private void AddBackground(Transform parent)
    {
        GameObject background = new GameObject("Background");
        background.transform.SetParent(parent, false);

        RectTransform bgRect = background.AddComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        Image bgImage = background.AddComponent<Image>();
        bgImage.color = Color.black; // Change to desired background color or sprite
    }

    private GameObject CreateButton(Transform parent, string name, string buttonText, Vector2 position)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent, false);

        RectTransform rectTransform = buttonObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 80);
        rectTransform.anchoredPosition = position;
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        Button button = buttonObject.AddComponent<Button>();
        buttonObject.AddComponent<Image>().color = Color.gray; // Set button background color

        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform, false);

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
        textObject.transform.SetParent(parent, false);

        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.color = color;
        text.alignment = TextAlignmentOptions.Center;

        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 100);
        rectTransform.anchoredPosition = position;
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }
}