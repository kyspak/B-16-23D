
using Pool;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

namespace Enemies
{
    public class DestroyByContact : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private GameObject _playerExplosion;
        
        private IDespawnable _selfDespawnable;

        private void Awake()
        {
            _selfDespawnable = GetComponent<IDespawnable>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Boundary"))
                return;
            
            if(_explosion)
                Instantiate(_explosion, transform.position, transform.rotation);

            if (other.CompareTag("Player"))
            {
                if(_playerExplosion)
                    Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
            }
            
            DespawnTarget(other.gameObject);

            DespawnSelf();
        }

        private void DespawnTarget(GameObject target)
        {
            if(target.TryGetComponent<IDespawnable>(out var despawnable))
                despawnable.Despawn();
            else
            {
                Destroy(target);
            }
        }

        private void DespawnSelf()
        {
            if (_selfDespawnable != null)
                _selfDespawnable.Despawn();
            else
                Destroy(gameObject);
        }
    }
}