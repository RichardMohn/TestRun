using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SetupScene : MonoBehaviour
{
    void Start()
    {
        // Remove existing canvases
        RemoveExistingCanvases();

        // Create the Main Menu Canvas
        CreateMainMenuCanvas();
    }

    void RemoveExistingCanvases()
    {
        GameObject startScreenCanvas = GameObject.Find("StartScreenCanvas");
        GameObject gameOverCanvas = GameObject.Find("GameOverCanvas");

        if (startScreenCanvas != null) Destroy(startScreenCanvas);
        if (gameOverCanvas != null) Destroy(gameOverCanvas);
    }

    void CreateMainMenuCanvas()
    {
        // Create a new Main Menu Canvas
        GameObject mainMenuCanvas = CreateCanvas("MainMenuCanvas");

        // Add Game Title
        CreateText(mainMenuCanvas.transform, "GameTitle", "Ball Block", new Vector2(0, 200), 60);

        // Add Main Menu Buttons
        GameObject singlePlayerButton = CreateButton(mainMenuCanvas.transform, "SinglePlayerButton", "Single Player", new Vector2(0, 50));
        GameObject multiplayerButton = CreateButton(mainMenuCanvas.transform, "MultiplayerButton", "Multiplayer", new Vector2(0, -50));
        GameObject settingsButton = CreateButton(mainMenuCanvas.transform, "SettingsButton", "Settings", new Vector2(0, -150));

        // Attach functionality to buttons
        singlePlayerButton.GetComponent<Button>().onClick.AddListener(() => ShowSinglePlayerMenu(mainMenuCanvas));
        multiplayerButton.GetComponent<Button>().onClick.AddListener(() => Debug.Log("Multiplayer menu clicked"));
        settingsButton.GetComponent<Button>().onClick.AddListener(() => Debug.Log("Settings menu clicked"));
    }

    void ShowSinglePlayerMenu(GameObject mainMenuCanvas)
    {
        // Disable Main Menu Canvas
        mainMenuCanvas.SetActive(false);

        // Create Single Player Canvas
        GameObject singlePlayerCanvas = CreateCanvas("SinglePlayerCanvas");

        // Add Single Player Title
        CreateText(singlePlayerCanvas.transform, "SinglePlayerTitle", "Single Player", new Vector2(0, 200), 50);

        // Add Endless Mode Button
        GameObject endlessModeButton = CreateButton(singlePlayerCanvas.transform, "EndlessModeButton", "Endless Mode", new Vector2(0, 50));
        endlessModeButton.GetComponent<Button>().onClick.AddListener(() => LoadEndlessMode());

        // Add Back Button
        GameObject backButton = CreateButton(singlePlayerCanvas.transform, "BackButton", "Back", new Vector2(0, -150));
        backButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            singlePlayerCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        });
    }

    void LoadEndlessMode()
    {
        Debug.Log("Loading Endless Mode...");
        // Replace with your single-player scene name
        SceneManager.LoadScene("EndlessModeScene");
    }

    GameObject CreateCanvas(string name)
    {
        GameObject canvasObject = new GameObject(name);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Add Event System if it doesn't exist
        if (GameObject.Find("EventSystem") == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        return canvasObject;
    }

    void CreateText(Transform parent, string name, string content, Vector2 position, int fontSize)
    {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(parent);

        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 100);
        rectTransform.anchoredPosition = position;
    }

    GameObject CreateButton(Transform parent, string name, string buttonText, Vector2 position)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent);

        Button button = buttonObject.AddComponent<Button>();
        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.2f, 0.6f, 1f); // Light blue button color

        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 50);
        rectTransform.anchoredPosition = position;

        GameObject textObject = new GameObject("ButtonText");
        textObject.transform.SetParent(buttonObject.transform);

        TextMeshProUGUI buttonTextComponent = textObject.AddComponent<TextMeshProUGUI>();
        buttonTextComponent.text = buttonText;
        buttonTextComponent.fontSize = 24;
        buttonTextComponent.alignment = TextAlignmentOptions.Center;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(200, 50);
        textRect.anchoredPosition = Vector2.zero;

        return buttonObject;
    }
}