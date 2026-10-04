using System;
using Pool;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private AsteroidPool _asteroidPool;
        private BulletPool _bulletPool;
        
        [Inject]
        public void Construct(AsteroidPool asteroidPool, BulletPool bulletPool)
        {
            _asteroidPool = asteroidPool;
            _bulletPool = bulletPool;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDespawnable>(out var despawnable))
            {
               despawnable.Despawn();
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}