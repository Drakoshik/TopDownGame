using System;
using System.Collections;
using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;

namespace GameArchitecture.Enemy
{
    public class EnemyLegacy : MonoBehaviour
    {
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _maxHealthPoints;
        [SerializeField] private bool _isFollow;
        [SerializeField] private bool _isShoot;
        [SerializeField] private EnemyProjectile _enemyProjectile;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private Transform _hpHolder;
        
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _bulletSpeed = 5f;


        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        public Action<float, float> OnTakeDamage;
        
        private Transform _target;
        private float _currentHealthPoints;
        private bool _isCanAttack;
        private ObjectPool<EnemyProjectile> _bulletPool;
        

        private void Start()
        {
            if(!_isShoot) return;
            _bulletPool = new ObjectPool<EnemyProjectile>(_enemyProjectile,
                3, true);
        }

        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentHealthPoints = _maxHealthPoints;
            _animator = GetComponent<Animator>();
            _animator.enabled = true;
            _isCanAttack = true;
            OnTakeDamage?.Invoke(_currentHealthPoints, _maxHealthPoints);
        }

        private void OnDisable()
        {
            if(!_isShoot) return;
            if(_bulletPool == null) return;
            _bulletPool.HideAllElements();
        }

        private void FixedUpdate()
        {
            if(!_isFollow) return;
            _target = SceneArchitect.Instance.GetCurrentTarget();
            Vector2 rotateVector = transform.position - _target.transform.position;
            transform.position = Vector2.MoveTowards(transform.position,
                _target.transform.position, _speed * Time.deltaTime);
            
            // var angle = Mathf.Atan2(rotateVector.y,
            //     rotateVector.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // if(!_isShoot) return;
            // if(!_isCanAttack) return;
            // Shoot(-rotateVector.normalized);
        }


        public void GetDamage(float damage)
        {
            _currentHealthPoints -= damage;
            if (_currentHealthPoints <= 0)
            {
                gameObject.SetActive(false);
                _animator.enabled = false;
            }
            OnTakeDamage?.Invoke(_currentHealthPoints, _maxHealthPoints);
        }

        private void Shoot(Vector2 direction)
        {
            StartCoroutine(Delay(_attackDelay));
            var bullet = _bulletPool.GetFreeElement();
            bullet.transform.position = _bulletSpawnPosition.position;
            var spreadDirection = GetAngleVector(direction, 0);
            bullet.Rigidbody.velocity = new Vector2(spreadDirection.x * _bulletSpeed,
                spreadDirection.y * _bulletSpeed);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        }
        
        protected IEnumerator Delay(float delay)
        {
            _isCanAttack = false;
            yield return new WaitForSeconds(delay);
            _isCanAttack = true;
        }

        private Vector2 GetAngleVector(Vector2 vector, float angle)
        {
            var rotatedX = vector.x * MathF.Cos(angle) - vector.y * MathF.Sin(angle);
            var rotatedY = vector.x * MathF.Sin(angle) + vector.y * MathF.Cos(angle);
            return new Vector2(rotatedX, rotatedY);
        }
    }
    
}
