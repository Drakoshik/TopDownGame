using System;
using GameArchitecture.Interfaces.ForDealDamage;
using GameArchitecture.NPCComponents;
using UnityEngine;

namespace GameArchitecture.Enemy
{
    public class Enemy : MonoBehaviour, IDealsDamageToPlayer
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private float _maxHealthPoints;
        [SerializeField] private FollowComponent _followComponent;
        [SerializeField] private PatrolComponent _patrolComponent;
        
        private float _currentHealthPoints;


        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentHealthPoints = _maxHealthPoints;
            _animator = GetComponent<Animator>();
            _patrolComponent.ResetPatrolTime();
        }

        private void Update()
        {
            // _followComponent.Follow(this.transform, SceneArchitect.Instance.GetCurrentTarget());

            print(_patrolComponent.Patrol(this.transform));
            
        }
    }
}
