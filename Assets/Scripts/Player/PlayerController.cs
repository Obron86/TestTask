using System;
using Core;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private TouchJoystick _touchJoystick;
        private Rigidbody _rigidBody;
        private Vector3 _moveDirection;
        private float _currentSpeed;
        private float _minX, _maxX, _minZ, _maxZ;
        private float _playerHalfWidth, _playerHalfLength;

        private bool _isGameOver;
    
        public event Action OnPlayerCollision;

        public void Initialize(GameObject plane, TouchJoystick touchJoystick, GameSettings gameSettings)
        {
            _playerModel = new PlayerModel { speed = gameSettings.playerSpeed};
            _touchJoystick = touchJoystick;
            _rigidBody = GetComponent<Rigidbody>();

            CalculateBounds(plane);

            var playerCollider = GetComponent<Collider>();
            CountPlayerSize(playerCollider);

            GlobalGameEvents.GameOver += OnGameOver;
        }

        private void CountPlayerSize(Collider playerCollider)
        {
            var bounds = playerCollider.bounds;
            _playerHalfWidth = bounds.size.x / 2;
            _playerHalfLength = bounds.size.z / 2;

            _minX += _playerHalfWidth;
            _maxX -= _playerHalfWidth;
            _minZ += _playerHalfLength;
            _maxZ -= _playerHalfLength;
        }

        private void CalculateBounds(GameObject plane)
        {
            (var minX, var maxX, var minZ, var maxZ) = PlaneUtilities.CalculatePlaneBoundaries(plane);
            _minX = minX;
            _maxX = maxX;
            _minZ = minZ;
            _maxZ = maxZ;
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
            Move();
        }

        private void ClampMovementTakingInAccountPlayerSize()
        {
            Vector3 newPosition = _rigidBody.position;
            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, _minZ, _maxZ);
            _rigidBody.position = newPosition;
        }

        private void ClampMovementByTheEdge()
        {
            Vector3 clampedVelocity = _rigidBody.velocity;
            if (_rigidBody.position.x <= _minX && clampedVelocity.x < 0 ||
                _rigidBody.position.x >= _maxX && clampedVelocity.x > 0)
            {
                clampedVelocity.x = 0;
            }

            if (_rigidBody.position.z <= _minZ && clampedVelocity.z < 0 ||
                _rigidBody.position.z >= _maxZ && clampedVelocity.z > 0)
            {
                clampedVelocity.z = 0;
            }

            _rigidBody.velocity = clampedVelocity;
            ClampMovementTakingInAccountPlayerSize();
        }

        private void Move()
        {
            _rigidBody.AddForce(_moveDirection * _currentSpeed, ForceMode.Acceleration);
            ClampMovementByTheEdge();
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
}
