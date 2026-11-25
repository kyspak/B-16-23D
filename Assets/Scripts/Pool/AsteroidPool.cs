using Enemies;
using UnityEngine;

namespace Pool
{
    public class AsteroidPool : MonoBehaviour, IPool<Asteroid>
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private int _prewarmCount = 20;
        [SerializeField] private int _maxSize = 100;
        [SerializeField] private Transform _container;

        private ObjectPool<Asteroid> _pool;
        public Asteroid Get() => Get(transform.position, transform.rotation);

        private void Awake()
        {
            if (!_container)
                _container = transform;

            _pool = new ObjectPool<Asteroid>(
                factory: CreateAsteroid,
                destroyAction: asteroid => Destroy(asteroid.gameObject),
                prewarmCount: _prewarmCount,
                maxSize: _maxSize);

        }

        public Asteroid Get(Vector3 position, Quaternion rotation)
        {
            Asteroid asteroid = _pool.Get();
            asteroid.transform.SetPositionAndRotation(position, rotation);
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        public void Release(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
            _pool.Release(asteroid);
        } 

        private Asteroid CreateAsteroid()
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab, _container);
            asteroid.OwnerPool = this;
            asteroid.gameObject.SetActive(false);
            return asteroid;
        }

        private void OnDestroy() => _pool.ReleaseAll();
    }
}