using System;
using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public class SubmachineGun : Weapon, IReloadable
    {
        [field: SerializeField] public int Magazine { get; set; }
        [field: SerializeField] public float ReloadTime { get; set; }
        [SerializeField] private bool _isCanShootAuto;
        [SerializeField] protected Transform BulletSpawnPlace;
        [SerializeField] protected float BulletSpeed;
        [SerializeField] private bool _isInfiniteAmmo;
        
        [SerializeField] private Projectile _bulletPrefab;
        
        protected bool IsAuto = false;
        public float CurrentMagazine { get; set; }
        
        protected ObjectPool<Projectile> BulletPool;

        private Vector2 _currentDirection;

        public override void Start()
        {
            base.Start();
            BulletPool = new ObjectPool<Projectile>(_bulletPrefab,
                3, true);
            CurrentMagazine = Magazine;
        }

        private void OnDisable()
        {
            BulletPool.HideAllElements();
        }

        public override void Attack(Vector2 direction)
        {
            if(!CanAttack) return;
            _currentDirection = direction;
            StartCoroutine(Delay(AttackDelay));
            var bullet = BulletPool.GetFreeElement();
            bullet.SetDamage(Damage);
            bullet.transform.position = BulletSpawnPlace.position;
            var spreadDirection = GetAngleVector(direction, 0);
            bullet.Rigidbody.velocity = new Vector2(spreadDirection.x * BulletSpeed,
                spreadDirection.y * BulletSpeed);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void Update()
        {
            if(!_isCanShootAuto) return;
            if(!IsAuto) return;
            Attack(_currentDirection);
        }


        public void Reload()
        {
            if(!CanAttack) return;
            StartCoroutine(Delay(ReloadTime));
            Ammunition -= CurrentMagazine;
            CurrentMagazine = Magazine;
        }
        
        private Vector2 GetAngleVector(Vector2 vector, float angle)
        {
            var rotatedX = vector.x * MathF.Cos(angle) - vector.y * MathF.Sin(angle);
            var rotatedY = vector.x * MathF.Sin(angle) + vector.y * MathF.Cos(angle);
            return new Vector2(rotatedX, rotatedY);
        }

        public void StartAuto()
        {
            IsAuto = true;
        }
        
        public void StopAuto()
        {
            IsAuto = false;
        }
        
        public void SetDirection(Vector2 direction)
        {
            _currentDirection = direction;
        }
    }
}
