using System.Collections;
using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float Damage;
        [SerializeField] protected float AttackDelay;
        [SerializeField] protected float Ammunition;
        
        [SerializeField] private bool needToFlip;

        protected bool CanAttack = true;
        protected SpriteRenderer Sprite;

        public virtual void Start()
        {
            Sprite = GetComponent<SpriteRenderer>();
        }

        public abstract void Attack(Vector2 direction);
        

        public void Flip(bool flip)
        {
            if(!needToFlip) return;
            if(flip == Sprite.flipY) return;
            Sprite.flipY = flip;
        }
        
        protected IEnumerator Delay(float delay)
        {
            CanAttack = false;
            yield return new WaitForSeconds(delay);
            CanAttack = true;
        }
    }
}
