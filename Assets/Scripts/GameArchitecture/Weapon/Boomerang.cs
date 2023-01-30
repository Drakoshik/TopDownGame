using System.Collections;
using GameArchitecture.Actions;
using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public class Boomerang : Weapon
    {
        [SerializeField] private Projectile bulletPrefab;
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform[] rightPoints = new Transform[2];
        [SerializeField] private Transform[] leftPoints = new Transform[2];
        [SerializeField] private float _value;

        protected ObjectPool<Projectile> BulletPool;

        private bool _canAttack = true;

        private float _attackDelayPoints;

        public override void Start()
        {
            base.Start();
            BulletPool = new ObjectPool<Projectile>(bulletPrefab,
                1, true);
        }
        public override void Attack(Vector2 direction)
        {
            if(!_canAttack) return;
            base.Attack(direction);
            _value = 0;
            var bullet = BulletPool.GetFreeElement();
            bullet.SetDamage(Damage);
            StartCoroutine(PlusValue(bullet.transform,
                transform.position ,
                rightPoints[0].position,
                rightPoints[1].position,
                endPoint.position));

        }

        private IEnumerator PlusValue(Transform obj, Vector3 startPoint,
            Vector3 firstPoint, Vector3 secondPoint, Vector3 endPoint)
        {
            ChangeCanRotateHolder.OnCanRotate?.Invoke(false);
            Sprite.enabled = false;
            _canAttack = false;

            while (_value <= 1)
            {
                yield return new WaitForSeconds(.01f);
                _value += .02f;
                obj.position = GetTrajectory(startPoint, firstPoint,
                    secondPoint,
                    endPoint, _value);
            }

            StartCoroutine(MinusValue(obj, endPoint));
        }

        private IEnumerator MinusValue(Transform obj, Vector3 endPoint)
        {
            while (_value >= 0)
            {
                yield return new WaitForSeconds(.01f);
                _value -= .02f;
                obj.position = GetTrajectory(transform.position, leftPoints[0].position,
                    leftPoints[1].position,
                    endPoint, _value);
            }
            
            obj.gameObject.SetActive(false);
            _canAttack = true;
            Sprite.enabled = true;
            ChangeCanRotateHolder.OnCanRotate?.Invoke(true);
        }


        private Vector3 GetTrajectory(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            Vector3 p01 = Vector3.Lerp(p0, p1, t);
            Vector3 p12 = Vector3.Lerp(p1, p2, t);
            Vector3 p23 = Vector3.Lerp(p2, p3, t);
            
            Vector3 p012 = Vector3.Lerp(p01, p12, t);
            Vector3 p123 = Vector3.Lerp(p12, p23, t);
            
            Vector3 p0123 = Vector3.Lerp(p012, p123, t);

            return p0123;
        }
    }
}
