using System;
using UnityEngine;

namespace Core
{
    public interface IWeapon
    {
        public GameObject shotPrefab { get; set; }
        public Transform shotSpawn  { get; set; }
        public float fireRate { get; set; }
        public event Action<Transform, GameObject> OnFire;
    }
}