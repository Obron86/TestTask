using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyModel _enemyModel;
    private PlayerController _playerController;
    private Rigidbody _enemyRigidbody;

    private bool _isGameOver;

    public void Initialize(EnemyModel enemyModel)
    {
        _enemyModel = enemyModel;
        GlobalGameEvents.GameOver += OnGameOver;
    }

    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void OnGameOver()
    {
        _isGameOver = true;
        GlobalGameEvents.GameOver -= OnGameOver;
    }

    void Update()
    {
        if (_isGameOver) return;
        Vector3 directionToPlayer = (_playerController.transform.position - transform.position).normalized;
        _enemyRigidbody.velocity = directionToPlayer * _enemyModel.speed;
    }
}