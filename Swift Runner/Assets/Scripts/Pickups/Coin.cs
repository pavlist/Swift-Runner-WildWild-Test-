using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;
    
    ScoreManager scoreManager;
    GameManager gameManager;

    public void Init(ScoreManager scoreManager, GameManager gameManager)
    {
        this.scoreManager = scoreManager;
        this.gameManager = gameManager;
    }

    protected override void OnPickup()
    {
        // Don't award points if game is over
        if (gameManager == null || !gameManager.GameOver)
        {
            scoreManager.IncreaseScore(scoreAmount);
        }
    }
}