using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    [Header("Gameplay UI")]
    [SerializeField] GameObject gameplayPanel;

    [Header("Loss Screen")]
    [SerializeField] GameObject lossScreenPanel;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text finalTimeText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] GameObject newHighScoreIndicator;
    [SerializeField] Button retryButton;

    GameManager gameManager;
    ScoreManager scoreManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        if (startButton != null) startButton.onClick.AddListener(OnStartButtonClicked);
        if (exitButton != null) exitButton.onClick.AddListener(OnExitButtonClicked);
        if (retryButton != null) retryButton.onClick.AddListener(OnRetryButtonClicked);
    }

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (lossScreenPanel != null) lossScreenPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowGameplay()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gameplayPanel != null) gameplayPanel.SetActive(true);
        if (lossScreenPanel != null) lossScreenPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowLossScreen(int finalScore, float finalTime)
    {
        if (lossScreenPanel != null)
        {
            lossScreenPanel.SetActive(true);
            
            if (finalScoreText != null) finalScoreText.text = $"Coins: {finalScore}";
            if (finalTimeText != null) finalTimeText.text = $"Time: {finalTime:F1}s";

            int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
            bool isNewHighScore = finalScore > savedHighScore;

            if (isNewHighScore)
            {
                PlayerPrefs.SetInt("HighScore", finalScore);
                PlayerPrefs.Save();
                if (newHighScoreIndicator != null) newHighScoreIndicator.SetActive(true);
            }
            else
            {
                if (newHighScoreIndicator != null) newHighScoreIndicator.SetActive(false);
            }

            if (highScoreText != null) 
                highScoreText.text = $"High Score: {Mathf.Max(finalScore, savedHighScore)}";
        }

        if (gameplayPanel != null) gameplayPanel.SetActive(false);
    }

    void OnStartButtonClicked()
    {
        ShowGameplay();
        if (gameManager != null) gameManager.StartGame();
    }

    void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnRetryButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
