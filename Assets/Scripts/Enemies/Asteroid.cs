using System;
using Pool;
using UnityEngine;
using IPoolable = Pool.IPoolable;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Asteroid : MonoBehaviour, IPoolable, IDespawnable
    {
        public AsteroidPool OwnerPool { get; set; }

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        public void OnSpawned()
        {
            if (_rb != null)
            {
                _rb.linearVelocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }

        public void OnDespawned()
        {
            if (_rb != null)
            {
                _rb.linearVelocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
            
        }

        public void Despawn()
        {
            if (OwnerPool != null)
                OwnerPool.Release(this);
            else
                Destroy(gameObject);
        }
    }
}
