using System;
using UnityEngine;

namespace GameArchitecture.Character
{
    public class TempPlayerSkin : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private SpriteRenderer Sprite;
        
        [SerializeField] private Sprite _startSprite;
        [SerializeField] private Sprite _deathSprite;

        private bool _canMove;
        private void Start()
        {
            SceneArchitect.OnPlayerDie += OnDieAction;
            _animator.enabled = false;
            _canMove = true;
        }

        private void FixedUpdate()
        {
            if(!_canMove)return;
            if (_player.GetMovementInput() != Vector2.zero)
            {
                _animator.enabled = true;
            }
            else
            {
                _animator.enabled = false;
            }

            var direction = _player.LookInput;
            if(direction == Vector2.zero) return;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Flip(MathF.Abs(angle) >= 90);
            
            
        }
        
        private void OnEnable()
        {
            Sprite.sprite = _startSprite;
            _canMove = true;
        }

        private void OnDieAction()
        {
            _animator.enabled = false;
            Sprite.sprite = _deathSprite;
            _canMove = false;
        }
        public void Flip(bool flip)
        {
            if(flip == Sprite.flipY) return;
            Sprite.flipY = flip;
        }
    }
}
