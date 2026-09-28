using Pool;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidPool _asteroidPool;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _spawnWidth;
    [SerializeField] private float _spawnRate;
    
    private float timer = 0f;
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
        Asteroid asteroid = _asteroidPool.Get();
        
        Vector3 position =
            _spawnPoint.position +
            _spawnPoint.right * Random.Range(-_spawnWidth / 2f, _spawnWidth / 2f);

        asteroid.transform.SetPositionAndRotation(
            position,
            _spawnPoint.rotation);

        asteroid.Launch();
    }


}
