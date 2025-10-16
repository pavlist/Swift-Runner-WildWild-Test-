using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float startTime = 30f;
    [SerializeField] float gameOverSlowMotion = 0.3f; 

    float timeLeft;
    float timeSurvived = 0f;
    bool gameStarted = false;
    bool gameOver = false;

    UIManager uiManager;
    ScoreManager scoreManager;

    public bool GameOver => gameOver;

    void Awake()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Start()
    {
        timeLeft = startTime;
        Time.timeScale = 1f; 
    }

    void Update()
    {
        if (gameStarted && !gameOver)
        {
            DecreaseTime();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        gameOver = false;
        timeLeft = startTime;
        timeSurvived = 0f;
        Time.timeScale = 1f; // Reset to normal speed
    }

    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }

    private void DecreaseTime()
    {
        timeLeft -= Time.deltaTime;
        timeSurvived += Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");

        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        gameOver = true;
        
        // Disable player input but keep the script running for visual movement
        playerController.DisableInput();
        
        // Keep level running but in slow motion
        
        // Set slow motion
        Time.timeScale = gameOverSlowMotion;

        int finalScore = scoreManager.GetScore();
        uiManager.ShowLossScreen(finalScore, timeSurvived);
    }
}
