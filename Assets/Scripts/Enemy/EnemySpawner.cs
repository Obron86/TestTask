using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab;
    private GameObject plane;
    private PlayerController player;
    private GameController gameController;
    private float spawnTimer;
    private float minX, maxX, minZ, maxZ;

    
    
    public void Initialize(GameController gameController, GameObject plane, GameObject enemyPrefab, PlayerController player)
    {
        this.gameController = gameController;
        this.plane = plane;
        this.enemyPrefab = enemyPrefab;
        this.player = player;

        // Calculate plane boundaries
        float planeHalfWidth = plane.transform.localScale.x * 5.0f;
        float planeHalfLength = plane.transform.localScale.z * 5.0f;

        minX = plane.transform.position.x - planeHalfWidth;
        maxX = plane.transform.position.x + planeHalfWidth;
        minZ = plane.transform.position.z - planeHalfLength;
        maxZ = plane.transform.position.z + planeHalfLength;
    }
    
    public void SetEnemyPrefab(GameObject enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= gameController.GameModel.enemySpawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX),
            plane.transform.position.y,
            Random.Range(minZ, maxZ)
        );

        EnemyController enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<EnemyController>();
        enemyInstance.Initialize(new EnemyModel());
        enemyInstance.SetPlayerController(player);
    }
}