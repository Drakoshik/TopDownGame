using System;
using UnityEngine;

namespace GameArchitecture.Enemy
{
    public class EnemyReset : MonoBehaviour
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private GameObject _enemy;

        private void OnEnable()
        {
            _enemy.transform.localPosition = _startPosition.localPosition;
            _enemy.gameObject.SetActive(true);
        }
    }
}
