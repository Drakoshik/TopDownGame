using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameArchitecture.Character
{
    public class PlayerInputRecorder : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _player;
        
        private float _timer;
        private bool _isShoot;
        private bool _isReload;
        private bool _isChangeWeapon;
        
        private void Start()
        {
            _player.InputActions.Player.Shoot.started += OnShoot;
            _player.InputActions.Player.Reload.started += OnReload;
            _player.InputActions.Player.ChangeWeapon.started += OnChangeWeapon;
            StartRecord();
        }

        public void StartRecord()
        {
            _timer = 0;
            PlayerReplayData.PlayerReplays.Add(new Dictionary<float, ReplayData>());
        }

        private void OnChangeWeapon(InputAction.CallbackContext obj)
        {
            _isChangeWeapon = true;
        }

        private void OnReload(InputAction.CallbackContext obj)
        {
            _isReload = true;
        }

        private void OnShoot(InputAction.CallbackContext obj)
        {
            _isShoot = true;
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         _timer = 0;
        //         PlayerReplayData.PlayerReplays.Add(new Dictionary<float, ReplayData>());
        //         print(PlayerReplayData.PlayerReplays.Count);
        //     }
        // }

        private void FixedUpdate()
        {
            if(PlayerReplayData.PlayerReplays.Count <= 0) return;
            _timer += Time.deltaTime;
            PlayerReplayData.PlayerReplays[PlayerReplayData.PlayerReplays.Count-1].
                Add(_timer, new ReplayData(_player.MoveInput, _player.LookInput,
                    _isShoot, _isReload, _isChangeWeapon));
            _isShoot = false;
            _isReload = false;
            _isChangeWeapon = false;
        }
    }
}
