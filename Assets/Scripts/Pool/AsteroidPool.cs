
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

        public Asteroid Get()
        {
            Asteroid asteroid = _pool.Get();
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }
        
        public void Release(Asteroid asteroid) => _pool.Release(asteroid);

        private Asteroid CreateAsteroid()
        {
            Asteroid asteroid = Instantiate(_asteroidPrefab, _container);
            asteroid.OwnerPool = this;
            return asteroid;
        }

        private void OnDestroy() => _pool.ReleaseAll();
    }
}