using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameModel gameModel;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemySpawner enemySpawner;

    public GameModel GameModel
    {
        get { return gameModel; }
    }

    void Start()
    {
        // Initialize game state
    }

    void Update()
    {
        // Update game state
    }
}