using Pool;
using UnityEngine;
using IPoolable = Pool.IPoolable;

namespace Combat
{
    public class Bullet : MonoBehaviour, IPoolable, IDespawnable
    {
        [SerializeField] private float _speed = 20f;
        
        public BulletPool OwnerPool { get; set; }
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Launch()
        {
            _rb.linearVelocity = transform.forward * _speed;
        }
        
        public void OnSpawned()
        {
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

        public void Despawn()
        {
            if (OwnerPool != null)
                OwnerPool.Release(this);
            else
                Destroy(gameObject);
        }
    }
}
