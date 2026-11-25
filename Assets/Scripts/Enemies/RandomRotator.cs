using UnityEngine;

namespace Enemies
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private float _minTumble;
        [SerializeField] private float _maxTumble;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();

            _rb.angularVelocity = Random.insideUnitSphere * Random.Range(_minTumble, _maxTumble);
        }
    }
}