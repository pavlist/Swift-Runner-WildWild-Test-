using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f;

    LevelGenerator levelGenerator;
    GameManager gameManager;

    public void Init(LevelGenerator levelGenerator, GameManager gameManager)
    {
        this.levelGenerator = levelGenerator;
        this.gameManager = gameManager;
    }

    protected override void OnPickup()
    {
        // Don't apply effect if game is over
        if (gameManager == null || !gameManager.GameOver)
        {
            levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        }
    }
}