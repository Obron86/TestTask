using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameModel gameModel;
    private PlayerController playerController;
    private EnemySpawner enemySpawner;

    public GameModel GameModel
    {
        get => gameModel;
    }

    public PlayerController PlayerController
    {
        get { return playerController; }
    }

    public void Initialize(PlayerController playerController)
    {
        this.playerController = playerController;
        gameModel = new GameModel();
    }
}