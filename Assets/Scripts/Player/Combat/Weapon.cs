
using System;
using Pool;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private float _fireRate;

        private float _nextFire;
        private BulletPool _bulletPool;
        
        private PlayerInput _playerInput;
        private InputAction _shotAction;
        
        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            
            _shotAction = _playerInput.actions["Attack"];
            
        }

        private void Update()
        {
            // if (_shotAction.triggered && Time.time >= _nextFire)
            // {
            //         
            //         _nextFire = Time.time + _fireRate;
            //         Spawn();
            // }
        }

        private void Spawn()
        {
            Bullet bullet = _bulletPool.Get();

            Vector3 position = _spawnPoint.position;

            bullet.transform.SetPositionAndRotation(
                position,
                _spawnPoint.rotation);

            bullet.Launch();
        }
    }
}
