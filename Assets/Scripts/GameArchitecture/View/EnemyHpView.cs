using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameArchitecture.View
{
    public class EnemyHpView : MonoBehaviour
    {
        [SerializeField] private Enemy.EnemyLegacy enemyLegacy;
        [SerializeField] private Image healthBar;

        private void Start()
        {
            enemyLegacy.OnTakeDamage += UpdateHealthBar;
        }


        private void UpdateHealthBar(float currentHealthPoints, float maxHealthPoints)
        {
            healthBar.fillAmount = currentHealthPoints / maxHealthPoints;
        }
    }
}
