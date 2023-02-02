using System;
using System.Collections;
using GameArchitecture.Character;
using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameArchitecture.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speed = 3;
        [SerializeField] private float _maxHealthPoints;
        [SerializeField] private bool _isFollow;
        [SerializeField] private bool _isShoot;
        [SerializeField] private EnemyProjectile _enemyProjectile;
        [SerializeField] private Transform _bulletSpawnPosition;
        
        [SerializeField] private bool needToFlip;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _bulletSpeed = 5f;



        public Action<float, float> OnTakeDamage;
        
        private SpriteRenderer Sprite;
        private GameObject _target;
        private float _currentHealthPoints;
        private bool _isCanAttack;
        private ObjectPool<EnemyProjectile> _bulletPool;
        private Animator _animator;

        private void Start()
        {
            Sprite = GetComponent<SpriteRenderer>();
            
            _target = FindObjectOfType<PlayerMovement>().gameObject;
            _bulletPool = new ObjectPool<EnemyProjectile>(_enemyProjectile,
                3, true);
            
        }

        private void OnEnable()
        {
            _currentHealthPoints = _maxHealthPoints;
            _animator = GetComponent<Animator>();
            _animator.enabled = true;
            _isCanAttack = true;
            OnTakeDamage?.Invoke(_currentHealthPoints, _maxHealthPoints);
        }

        private void FixedUpdate()
        {
            if(!_isFollow) return;
            transform.position = Vector2.MoveTowards(transform.position,
                _target.transform.position, _speed * Time.deltaTime);
            
            var angle = Mathf.Atan2(_target.transform.position.y,
                _target.transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Flip(MathF.Abs(angle) >= 90);
            if(!_isShoot) return;
            if(!_isCanAttack) return;
            Shoot();
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

        private void Shoot()
        {
            var direction = _target.transform.position.normalized;
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
        
        public void Flip(bool flip)
        {
            if(!needToFlip) return;
            if(flip == Sprite.flipY) return;
            Sprite.flipY = flip;
        }
        
        private Vector2 GetAngleVector(Vector2 vector, float angle)
        {
            var rotatedX = vector.x * MathF.Cos(angle) - vector.y * MathF.Sin(angle);
            var rotatedY = vector.x * MathF.Sin(angle) + vector.y * MathF.Cos(angle);
            return new Vector2(rotatedX, rotatedY);
        }
    }
    
}
