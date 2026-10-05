
using UnityEngine;

namespace Core
{
    public class WeaponController : MonoBehaviour
    {
        private IWeapon _weapon;
        
        private float _nextFire;

        public void Awake()
        {
            _weapon = GetComponent<IWeapon>();
        }
        
        private void OnEnable()
        {
            _weapon.OnFire += Fire;
        }

        private void OnDisable()
        {
            _weapon.OnFire -= Fire;
        }

        private void Fire(Transform target, GameObject bullet)
        {
            Instantiate(bullet, target.position, target.rotation);
        }
    }
}