using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows.WebCam;

namespace GameArchitecture.Character
{
    public class PlayerInputRecorder : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _player;
        
        private float _timer;
        private bool _isShoot;
        private bool _isReload;
        private bool _isChangeWeapon;
        private bool _isShootCancel;

        private bool _isRecording;
        private void Start()
        {
            _player.InputActions.Player.Shoot.started += OnShoot;
            _player.InputActions.Player.Shoot.canceled += OnShootCancel;
            _player.InputActions.Player.Reload.started += OnReload;
            _player.InputActions.Player.ChangeWeapon.started += OnChangeWeapon;

            SceneArchitect.OnPlayerDie += StopRecording;
        }

        private void StopRecording()
        {
            _isRecording = false;
        }

        private void OnShootCancel(InputAction.CallbackContext obj)
        {
            _isShootCancel = true;
        }

        public void StartRecord()
        {
            _isRecording = true;
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

        private void FixedUpdate()
        {
            if(!_isRecording) return;
            if(PlayerReplayData.PlayerReplays.Count <= 0) return;
            _timer += Time.deltaTime;
            PlayerReplayData.PlayerReplays[PlayerReplayData.PlayerReplays.Count-1].
                Add(_timer, new ReplayData(_player.MoveInput, _player.LookInput,
                    _isShoot, _isReload, _isChangeWeapon, _isShootCancel));
            _isShoot = false;
            _isReload = false;
            _isChangeWeapon = false;
            _isShootCancel = false;
        }
    }
}
