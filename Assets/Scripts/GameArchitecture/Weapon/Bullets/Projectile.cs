using System.Collections;
using UnityEngine;
using GameArchitecture.Enemy;

namespace GameArchitecture.Weapon.Bullets
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody { get; private set; }
        [SerializeField] private Sprite image;
        [SerializeField] private float lifecycleTime = 2f;
        [SerializeField] private bool needToHide;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private float _damage;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _spriteRenderer.sprite = image;
            StartCoroutine(DeathTimer());
        }
        

        private IEnumerator DeathTimer()
        {
            yield return new WaitForSeconds(lifecycleTime);
            if (_animator != null)
            {
                _animator.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.GetComponent<Enemy.EnemyLegacy>()) 
                col.GetComponent<Enemy.EnemyLegacy>().GetDamage(_damage);
            if(needToHide) gameObject.SetActive(false);
            if(_animator == null) return;
            if(col.GetComponent<Projectile>()) return;
            _animator.enabled = true;
            Rigidbody.velocity = Vector2.zero;
        }

        private void DisableAnimator()
        {
            _animator.enabled = false;
            gameObject.SetActive(false);
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}
