using Enemies;
using Pool;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private AsteroidPool _asteroidPool;
        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private float _spawnWidth;
        [SerializeField] private float _spawnRate;
        
        private float timer = 0f;

        [Inject]
        public void Construct(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }
        
        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= _spawnRate)
            {
                Spawn();
                
                timer = 0f; 
            }
        }

        private void Spawn()
        {

            Vector3 position =
                _spawnPoint.position +
                _spawnPoint.right * Random.Range(-_spawnWidth / 2f, _spawnWidth / 2f);
            
            Asteroid asteroid = _asteroidPool.Get(position,_spawnPoint.rotation);
        }
    }
}
