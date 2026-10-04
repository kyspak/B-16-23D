using UnityEngine;

namespace Enemies
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.linearVelocity = Vector3.zero;
        }

        private void OnEnable()
        {
            _rb.linearVelocity = transform.forward * Random.Range(_minSpeed, _maxSpeed);
        }
        
    }
}