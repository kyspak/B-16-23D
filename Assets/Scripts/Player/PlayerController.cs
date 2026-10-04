using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _tilt;
        [SerializeField] private Boundary _boundary;
        
        [SerializeField] private float _fireRate;

        private float _nextFire;
        
        private Rigidbody _rb;
        private PlayerInput _playerInput;
        
        private InputAction _moveAction;
        private InputAction _shotAction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerInput = GetComponent<PlayerInput>();
            
            _moveAction = _playerInput.actions["Move"];
        }

        private void Update()
        {
            // if (_shotAction.triggered)
            // {
            //     _nextFire = Time.time + _fireRate;
            //     Instantiate(_shotPrefab, _shotSpawn.position, _shotSpawn.rotation);
            //
            //     _shotAudio.Play();
            // }
        }

        private void FixedUpdate()
        {
            Vector2 move = _moveAction.ReadValue<Vector2>();
            
            Vector3 movement = new Vector3(move.x, 0.0f, move.y);
            _rb.linearVelocity = movement * _speed;

            _rb.position = new Vector3(
                Mathf.Clamp(_rb.position.x, _boundary.xMin, _boundary.xMax),
                0.0f,
                Mathf.Clamp(_rb.position.z, _boundary.zMin, _boundary.zMax)
            );

            _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.linearVelocity.x * -_tilt);
            
        }
    }
}
