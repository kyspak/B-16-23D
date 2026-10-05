
using System;
using System.Collections;
using Combat;
using Core;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyShipController : MonoBehaviour, IWeapon
    {
        [SerializeField] private float tilt;
        [SerializeField] private Boundary boundary;


        [SerializeField] private float dodge;
        [SerializeField] private float smoothing;
        [SerializeField] private Vector2 startWait;
        [SerializeField] private Vector2 maneuverTime;
        [SerializeField] private Vector2 maneuverWait;

        [SerializeField] private GameObject _shotPrefab;
        [SerializeField] private Transform _shotSpawn;
        [SerializeField] private float _fireRate;

        [SerializeField] public float delay;
        
        private float currentSpeed;
        private float _targetManeuver;
        
        private Rigidbody _rb;
        
        public GameObject shotPrefab
        {
            get => _shotPrefab;
            set => _shotPrefab = value;
        }

        public Transform shotSpawn
        {
            get => _shotSpawn;
            set => _shotSpawn = value;
        }

        public float fireRate
        {
            get => _fireRate;
            set => _fireRate = value;
        }

        public event Action<Transform, GameObject> OnFire;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            currentSpeed = _rb.linearVelocity.z;
            StartCoroutine(Evade());
            
            InvokeRepeating("Fire", delay, _fireRate);
        }

        private void Fire()
        {
            OnFire?.Invoke(_shotSpawn, _shotPrefab);
        }

        private IEnumerator Evade()
        {
            yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

            while (true)
            {
                _targetManeuver = Random.Range(1, dodge) * Mathf.Sign(transform.position.x);
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
                _targetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            }
        }

        private void FixedUpdate()
        {
            float newManeuver = Mathf.MoveTowards(_rb.linearVelocity.x,
                _targetManeuver,
                smoothing * Time.fixedDeltaTime);

            _rb.linearVelocity = new Vector3(newManeuver, 0.0f, currentSpeed);
            _rb.position = new Vector3
            (
                Mathf.Clamp(_rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(_rb.position.z, boundary.zMin, boundary.zMax)
            );
            
            _rb.rotation = Quaternion.Euler(0,0, _rb.linearVelocity.x * -tilt);
        }


    }
}