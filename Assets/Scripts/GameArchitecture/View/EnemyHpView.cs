using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameArchitecture.View
{
    public class EnemyHpView : MonoBehaviour
    {
        [SerializeField] private Enemy.Enemy _enemy;
        [SerializeField] private Image healthBar;

        private void Start()
        {
            _enemy.OnTakeDamage += UpdateHealthBar;
        }


        private void UpdateHealthBar(float currentHealthPoints, float maxHealthPoints)
        {
            healthBar.fillAmount = currentHealthPoints / maxHealthPoints;
        }
    }
}
