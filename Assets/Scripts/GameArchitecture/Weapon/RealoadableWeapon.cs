using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;


namespace GameArchitecture.Weapon
{
    public class RealoadableWeapon : Weapon
    {
        [SerializeField] protected float Clip;
        [SerializeField] protected float ReloadTime;
        [SerializeField] protected int ProjectileCount;
        [SerializeField] protected Transform BulletSpawnPlace;
        [SerializeField] protected float BulletSpeed;
        
        [SerializeField] private Projectile _bulletPrefab;
        [SerializeField] private float _currentClip;
        

        protected ObjectPool<Projectile> BulletPool;

        private void OnEnable()
        {
            CanAttack = true;
        }

        public override void Attack(Vector2 direction)
        {
            if(_currentClip <= 0) Reload();
            
            if(!CanAttack) return;
            StartDelay(AttackDelay);
            _currentClip--;
        }

        public void Reload()
        {
            if(!CanAttack) return;
            _currentClip = Clip;
            Ammunition -= Clip;
            StartDelay(ReloadTime);
        }

        public override void Start()
        {
            base.Start();
            BulletPool = new ObjectPool<Projectile>(_bulletPrefab,
                3, true);
            _currentClip = Clip;
        }

        private void StartDelay(float delay)
        {
            CanAttack = false;
            StartCoroutine(Delay(delay));
        }


        // private IEnumerator Delay(float delay)
        // {
        //     yield return new WaitForSeconds(delay);
        //     CanAttack = true;
        //     print(CanAttack);
        // }
    }
}
