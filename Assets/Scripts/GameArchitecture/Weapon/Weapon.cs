using GameArchitecture.Pool;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected float Damage;
        [SerializeField] protected float AttackDelay;
        
        [SerializeField] private bool needToFlip;

        protected SpriteRenderer Sprite;

        public virtual void Start()
        {
            Sprite = GetComponent<SpriteRenderer>();
        }


        public virtual void Attack(Vector2 direction) { }

        public void Flip(bool flip)
        {
            if(!needToFlip) return;
            if(flip == Sprite.flipY) return;
            Sprite.flipY = flip;
        }
    }
}
