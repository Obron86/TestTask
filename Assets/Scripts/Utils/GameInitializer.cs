using UnityEngine;
using UnityEngine.Serialization;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject gameControllerPrefab;
    [SerializeField] private GameObject playerControllerPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;
    [SerializeField] private GameObject floorPrefab;
    
    private GameController gameController;
    private PlayerController playerController;
    private EnemySpawner enemySpawner;
    private GameObject floor;

    void Start()
    {
        // Instantiate prefabs
        floor = Instantiate(floorPrefab);
        gameController = Instantiate(gameControllerPrefab).GetComponent<GameController>();
        playerController = Instantiate(playerControllerPrefab).GetComponent<PlayerController>();
        enemySpawner = Instantiate(enemySpawnerPrefab).GetComponent<EnemySpawner>();

        gameController.Initialize(playerController);
        playerController.Initialize(floor);
        enemySpawner.Initialize(gameController, floor, enemyPrefab, playerController);
        enemySpawner.SetEnemyPrefab(enemyPrefab);
    }

}
