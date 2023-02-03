using System;
using UnityEngine;

namespace GameArchitecture.Character.PlayerClone
{
    public class TempCloneSkin : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CloneController _clone;
        [SerializeField] private SpriteRenderer Sprite;
        
        [SerializeField] private Sprite _startSprite;
        [SerializeField] private Sprite _deathSprite;
        

        [SerializeField] private Vector3 currentPosition;

        private void Start()
        {
            _animator.enabled = false;
            _clone.OnLook += ChangeDirection;
            currentPosition = _clone.transform.localPosition;
            _clone.OnEndOfLife += OnDieAction;
        }

        

        private void ChangeDirection(Vector3 direction)
        {
            if (currentPosition != _clone.transform.localPosition)
            {
                _animator.enabled = true;
                currentPosition = _clone.transform.localPosition;
            }
            else
            {
                _animator.enabled = false;
            }
            
            if(direction == Vector3.zero) return;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Flip(MathF.Abs(angle) >= 90);
        }
        
        private void OnEnable()
        {
            Sprite.sprite = _startSprite;
        }

        private void OnDieAction()
        {
            Sprite.sprite = _deathSprite;
        }
        
        
        public void Flip(bool flip)
        {
            if(flip == Sprite.flipY) return;
            Sprite.flipY = flip;
        }
    }
}
