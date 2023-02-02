using System;
using UnityEngine;

namespace GameArchitecture.Weapon.Bullets
{
    public class EnemyProjectile : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            gameObject.SetActive(false);
        }
    }
}
