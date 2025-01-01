using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneSetup : MonoBehaviour
{
    private static bool hasRun = false;

    void Start()
    {
        if (hasRun) return;
        hasRun = true;

        // Create the Main Menu UI
        CreateMainMenu();
    }

    private void CreateMainMenu()
    {
        // Create Main Menu Canvas
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

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
        CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 200));
        CreateButton(mainMenuCanvas.transform, "MultiplayerButton", "Multiplayer", new Vector2(0, 100));
        CreateButton(mainMenuCanvas.transform, "SettingsButton", "Settings", new Vector2(0, 0));

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
        text.color = color;
        text.alignment = TextAlignmentOptions.Center;

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 100);
        rectTransform.anchoredPosition = position;
    }
}