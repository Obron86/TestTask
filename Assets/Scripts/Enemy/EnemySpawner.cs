using Core;
using Player;
using Settings;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private GameObject _enemyPrefab;
        private PlayerController _player;
        private GameController _gameController;
        private float _spawnTimer;
        private float _minX, _maxX, _minZ, _maxZ;
        private bool _isGameOver;
        private int _enemySpawned;
        private GameSettings _gameSettings;

        public void Initialize(GameController gameController, GameObject plane, GameObject enemyPrefab,
            PlayerController player, GameSettings gameSettings)
        {
            _gameController = gameController;
            _enemyPrefab = enemyPrefab;
            _player = player;
            _gameSettings = gameSettings;

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
            if (_spawnTimer >= _gameController.GameModel.enemySpawnRate && _enemySpawned < _gameSettings.maxEnemies)
            {
                SpawnEnemy();
                _enemySpawned++;
                _spawnTimer = 0;
            }
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(_minX, _maxX),
                0,
                Random.Range(_minZ, _maxZ)
            );

            EnemyController enemyInstance = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity)
                .GetComponent<EnemyController>();
            enemyInstance.Initialize(new EnemyModel { speed = _gameSettings.enemySpeed });
            enemyInstance.SetPlayerController(_player);
        }
    }
}