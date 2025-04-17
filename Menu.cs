using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject CanvasMain;
    public GameObject CanvasSettings;
    public GameObject CanvasPause;
    public GameObject CanvasGameOver;
    public GameObject registerCanvas;
    public GameObject loginCanvas;
    public GameObject finishCanvas;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        CanvasSettings.SetActive(false);
        CanvasMain.SetActive(true);
        CanvasPause.SetActive(false);
        CanvasGameOver.SetActive(false);
        finishCanvas.SetActive(false);
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(false);

        if (PlayerPrefs.GetInt("StartGameDirectly", 0) == 1)
        {
            StartGame();
            PlayerPrefs.SetInt("StartGameDirectly", 0);
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default to 1 if not set
        int nextLevel = currentLevel + 1;
        if (nextLevel > 3) nextLevel = 1; // Loop back to Level 1 after Level 3
        PlayerPrefs.SetInt("CurrentLevel", nextLevel);
        SceneManager.LoadScene("Level" + nextLevel); // Ensure scene names match (e.g., "Level2")
    }

    public void StartGame()
    {
        CanvasMain.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasPause.SetActive(false);
        CanvasGameOver.SetActive(false);
        finishCanvas.SetActive(false);
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowLoginCanvas()
    {
        loginCanvas.SetActive(true);
        registerCanvas.SetActive(false);
    }

    public void ShowRegisterCanvas()
    {
        registerCanvas.SetActive(true);
        loginCanvas.SetActive(false);
    }

    public void OpenSettings()
    {
        CanvasSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        CanvasSettings.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}