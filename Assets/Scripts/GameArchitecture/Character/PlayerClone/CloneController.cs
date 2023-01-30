using System;
using UnityEngine;

namespace GameArchitecture.Character.PlayerClone
{
    public class CloneController : MonoBehaviour
    {
        public event Action<Vector3> OnLook; 
        public event Action<Vector3> OnShoot; 
        public event Action OnReload; 
        public event Action OnChangeWeapon; 
        
        
        
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

        private void OnEnable()
        {
            _inScriptTimer = 0;
        }

        public void SetCloneNumber(int cloneNumber)
        {
            _cloneNumber = cloneNumber;
        }

        private void FixedUpdate()
        {
            
            if(PlayerReplayData.PlayerReplays.Count <= 0) return;
            _inScriptTimer += Time.deltaTime;
            if (!PlayerReplayData.PlayerReplays[_cloneNumber].ContainsKey(_inScriptTimer))
            {
                EndOfLifetimeAction();
                return;
            }
            var playerReplayData = PlayerReplayData.PlayerReplays[_cloneNumber]
                [_inScriptTimer];
            _playerAnimator.SetFloat(LastX, playerReplayData.LookInput.x);
            _playerAnimator.SetFloat(LastY, playerReplayData.LookInput.y);

            transform.position = (Vector3)playerReplayData.MovementInput;
            _playerAnimator.SetFloat(MovementX, playerReplayData.MovementInput.x);
            _playerAnimator.SetFloat(MovementY, playerReplayData.MovementInput.y);
            
            InvokeActions(playerReplayData);
        }

        private void EndOfLifetimeAction()
        {
            gameObject.SetActive(false);
        }

        private void InvokeActions(ReplayData playerReplayData)
        {
            OnLook?.Invoke(playerReplayData.LookInput);

            if (playerReplayData.IsShoot) OnShoot?.Invoke(playerReplayData.LookInput);

            if (playerReplayData.IsReload) OnReload?.Invoke();

            if (playerReplayData.IsChangeWeapon) OnChangeWeapon?.Invoke();
        }
    }
}
