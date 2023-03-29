using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject _enemyPrefab;
    private GameObject _plane;
    private PlayerController _player;
    private GameController _gameController;
    private float _spawnTimer;
    private float _minX, _maxX, _minZ, _maxZ;
    private bool _isGameOver;
    private int _spawnedEnemiesCounter;
    
    public void Initialize(GameController gameController, GameObject plane, GameObject enemyPrefab, PlayerController player)
    {
        _gameController = gameController;
        _plane = plane;
        _enemyPrefab = enemyPrefab;
        _player = player;

        var localScale = plane.transform.localScale;
        var planeHalfWidth = localScale.x * 5.0f;
        var planeHalfLength = localScale.z * 5.0f;

        (var minX, var maxX, var minZ, var maxZ) = PlaneUtilities.CalculatePlaneBoundaries(plane);
        _minX = minX;
        _maxX = maxX;
        _minZ = minZ;
        _maxZ = maxZ;
        
        GlobalGameEvents.GameOver += OnGameOver;
    }
    
    private void OnGameOver()
    {
        _isGameOver = true;
        GlobalGameEvents.GameOver -= OnGameOver;
    }
    
    public void SetEnemyPrefab(GameObject enemyPrefab) => _enemyPrefab = enemyPrefab;

    private void Update()
    {
        if (_isGameOver) return;
        _spawnTimer += Time.deltaTime;
        
        TrySpawn();
    }

    private void TrySpawn()
    {
        if (_spawnTimer >= _gameController.GameModel.enemySpawnRate &&
            _spawnedEnemiesCounter < _gameController.GameModel.maxEnemies)
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(_minX, _maxX),
            _plane.transform.position.y,
            Random.Range(_minZ, _maxZ)
        );

        _spawnedEnemiesCounter++;
        
        EnemyController enemyInstance = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<EnemyController>();
        enemyInstance.Initialize(new EnemyModel());
        enemyInstance.SetPlayerController(_player);
    }
}