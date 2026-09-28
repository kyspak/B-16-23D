using System;
using Pool;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class DestroyByBoundary : MonoBehaviour
    {
        private AsteroidPool _asteroidPool;
        
        [Inject]
        public void Construct(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }
        private void OnTriggerExit(Collider other)
        {
            //Destroy(other.gameObject);
            if (other.TryGetComponent<Asteroid>(out var asteroid))
            {
                if (asteroid.gameObject.activeSelf)
                {
                    _asteroidPool.Release(asteroid);
                }
            }
        }
    }
}