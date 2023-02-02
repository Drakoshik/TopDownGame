using System;
using UnityEngine;

namespace GameArchitecture.Enemy
{
    public class EnemyPointMovement : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _speed;
        [SerializeField] private SpriteRenderer[] _enemySprites;


        private void OnEnable()
        {
            _enemyContainer.position = _startPoint.position;
            var angle = Mathf.Atan2(_endPoint.transform.position.y,
                _endPoint.transform.position.x) * Mathf.Rad2Deg;
            _enemyContainer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            FlipSprites(MathF.Abs(angle) >= 90);
        }

        private void FixedUpdate()
        {
            _enemyContainer.position = Vector2.MoveTowards(_enemyContainer.position,
                _endPoint.transform.position, _speed * Time.deltaTime);
            if(_enemyContainer.position == _endPoint.transform.position) 
                _enemyContainer.gameObject.SetActive(false);
        }

        private void FlipSprites(bool flip)
        {
            foreach (var enemy in _enemySprites)
            {
                enemy.gameObject.SetActive(true);
                if(flip == enemy.flipY) continue;
                enemy.flipY = flip;
            }
            
        }
    }
}
