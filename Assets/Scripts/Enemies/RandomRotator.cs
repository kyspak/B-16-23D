using UnityEngine;

namespace Enemies
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private float _minTumble;
        [SerializeField] private float _maxTumble;

        private void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(_minTumble, _maxTumble);
        }
    }
}