using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;
    private GameObject _plane;
    private TouchJoystick _touchJoystick;
    private Rigidbody _rigidBody;
    private Vector3 _moveDirection;
    private float _currentSpeed;
    private float _minX, _maxX, _minZ, _maxZ;
    private float _playerHalfWidth, _playerHalfLength;

    private bool _isGameOver;
    
    public event Action OnPlayerCollision;

    public void Initialize(GameObject plane, TouchJoystick touchJoystick)
    {
        _playerModel = new PlayerModel();
        _touchJoystick = touchJoystick;
        this._plane = plane;
        _rigidBody = GetComponent<Rigidbody>();

        // Calculate plane boundaries
        var localScale = plane.transform.localScale;
        float planeHalfWidth = localScale.x * 5.0f;
        float planeHalfLength = localScale.z * 5.0f;

        var position = _plane.transform.position;
        _minX = position.x - planeHalfWidth;
        _maxX = position.x + planeHalfWidth;
        _minZ = position.z - planeHalfLength;
        _maxZ = position.z + planeHalfLength;

        // Calculate player size
        Collider playerCollider = GetComponent<Collider>();
        var bounds = playerCollider.bounds;
        _playerHalfWidth = bounds.size.x / 2;
        _playerHalfLength = bounds.size.z / 2;
        
        _minX += _playerHalfWidth;
        _maxX -= _playerHalfWidth;
        _minZ += _playerHalfLength;
        _maxZ -= _playerHalfLength;

        GlobalGameEvents.GameOver += OnGameOver;
    }

    void Update()
    {
        if (_isGameOver) return;
        Vector3 inputDirection = _touchJoystick.GetInputDirection();
        float speedPercentage = inputDirection.magnitude;
        _currentSpeed = _playerModel.speed * speedPercentage;

        _moveDirection = inputDirection.normalized;
        _moveDirection.y = 0;
    }

    void FixedUpdate()
    {
        if (_isGameOver) return;
        _rigidBody.AddForce(_moveDirection * _currentSpeed, ForceMode.Acceleration);

        // Clamp the rigidbody's velocity to prevent slow movement beyond the edge
        Vector3 clampedVelocity = _rigidBody.velocity;
        if (_rigidBody.position.x <= _minX && clampedVelocity.x < 0 || _rigidBody.position.x >= _maxX && clampedVelocity.x > 0)
        {
            clampedVelocity.x = 0;
        }
        if (_rigidBody.position.z <= _minZ && clampedVelocity.z < 0 || _rigidBody.position.z >= _maxZ && clampedVelocity.z > 0)
        {
            clampedVelocity.z = 0;
        }
        _rigidBody.velocity = clampedVelocity;

        // Clamp the rigidbody's position to stay within the plane boundaries
        Vector3 newPosition = _rigidBody.position;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, _minZ, _maxZ);
        _rigidBody.position = newPosition;
    }
    
    private void OnGameOver()
    {
        _isGameOver = true;
        GlobalGameEvents.GameOver -= OnGameOver;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnPlayerCollision?.Invoke();
        }
    }
}
