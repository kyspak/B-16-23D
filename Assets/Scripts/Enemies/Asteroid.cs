using Pool;
using UnityEngine;
using IPoolable = Pool.IPoolable;

public class Asteroid : MonoBehaviour, IPoolable
{
    public AsteroidPool OwnerPool { get; set; }
    
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    
    
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public void Launch()
    {
        _rb.linearVelocity =
            -transform.forward * Random.Range(_minSpeed, _maxSpeed);
    }
    
    public void OnSpawned()
    {
        if (_rb)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.ResetInertiaTensor();
        }
    }

    public void OnDespawned()
    {
        if (_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
        gameObject.SetActive(false);
    }
}
