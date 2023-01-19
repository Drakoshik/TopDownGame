using System;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public class Shotgun : RealoadableWeapon
    {
        [SerializeField] [field: Range(0f, 2f)] private float _spreading = 1f;

        public override void Attack(Vector2 direction)
        {
            if(!CanAttack) return;
            base.Attack(direction);
            var bulletSpread = _spreading / (ProjectileCount - 1);
            var spread = _spreading / 2;
            for (var i = 0; i < ProjectileCount; i++)
            {
                var bullet = BulletPool.GetFreeElement();
                bullet.SetDamage(Damage/ProjectileCount);
                bullet.transform.position = BulletSpawnPlace.position;
                var spreadDirection = GetAngleVector(direction, spread);
                bullet.Rigidbody.velocity = new Vector2(spreadDirection.x * BulletSpeed,
                    spreadDirection.y * BulletSpeed);
                spread -= bulletSpread;
            }
            
        }

        private Vector2 GetAngleVector(Vector2 vector, float angle)
        {
            var rotatedX = vector.x * MathF.Cos(angle) - vector.y * MathF.Sin(angle);
            var rotatedY = vector.x * MathF.Sin(angle) + vector.y * MathF.Cos(angle);
            return new Vector2(rotatedX, rotatedY);
        }
    }
}
