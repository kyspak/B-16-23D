using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerController : MonoBehaviour, IWeapon
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _tilt;
        [SerializeField] private Boundary _boundary;
        
        [SerializeField] private GameObject _shotPrefab;
        [SerializeField] private Transform _shotSpawn;
        [SerializeField] private float _fireRate;
        
        
        private Rigidbody _rb;
        private PlayerInput _playerInput;
        private AudioSource _shotAudio;
        
        private InputAction _moveAction;
        private InputAction _shotAction;


        public GameObject shotPrefab
        {
            get => _shotPrefab;
            set => _shotPrefab = value;
        }

        public Transform shotSpawn
        {
            get => _shotSpawn;
            set => _shotSpawn = value;
        }

        public float fireRate
        {
            get => _fireRate;
            set => _fireRate = value;
        }

        public event Action<Transform, GameObject> OnFire;

        //public event Action<Transform, GameObject> OnFire;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerInput = GetComponent<PlayerInput>();
            _shotAudio = GetComponent<AudioSource>();
            
            
            _shotAction = _playerInput.actions["Attack"];
            _moveAction = _playerInput.actions["Move"];
        }

        private void OnEnable()
        {
            _shotAction.started += _ => OnFireHandler();
        }

        private void OnDisable()
        {
            _shotAction.started -= _ => OnFireHandler();
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

        private void OnFireHandler()
        {
            OnFire?.Invoke( _shotSpawn, _shotPrefab);
            _shotAudio.Play();
        }
    }
}
