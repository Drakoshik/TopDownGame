using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameArchitecture.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        private float _healthPoints;

        private void Start()
        {
            _healthPoints = 100;
        }

        public void GetDamage(float damage)
        {
            _healthPoints -= damage;
            if (_healthPoints <= 0) _healthPoints = 100;
            healthBar.fillAmount = _healthPoints / 100;
        }
    }
}
