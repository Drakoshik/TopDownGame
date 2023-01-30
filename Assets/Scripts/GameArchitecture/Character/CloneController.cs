using System;
using UnityEngine;

namespace GameArchitecture.Character
{
    public class CloneController : MonoBehaviour
    {
        private int _cloneNumber;
        private float _inScriptTimer;
        
        private Animator _playerAnimator;
        
        private static readonly int LastX = Animator.StringToHash("LastX");
        private static readonly int LastY = Animator.StringToHash("LastY");
        private static readonly int MovementX = Animator.StringToHash("MovementX");
        private static readonly int MovementY = Animator.StringToHash("MovementY");
        
        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            _inScriptTimer = 0;
        }

        private void FixedUpdate()
        {
            if(PlayerReplayData.PlayerReplays.Count <= 0) return;
            _inScriptTimer += Time.deltaTime;
            _playerAnimator.SetFloat(LastX, PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer].LookInput.x);
            _playerAnimator.SetFloat(LastY, PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer].LookInput.y);

            transform.position = (Vector3)PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer].MovementInput;
            _playerAnimator.SetFloat(MovementX, PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer].MovementInput.x);
            _playerAnimator.SetFloat(MovementY, PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer].MovementInput.y);
            print(transform.position);
            
        }
        

        private void ReadLookInput()
        {
            
            
        }

        private void Move()
        {
            
            
        }
    }
}
