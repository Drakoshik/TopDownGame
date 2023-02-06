using System;
using System.Collections.Generic;
using GameArchitecture.Actions;
using GameArchitecture.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameArchitecture.Weapon
{
    public class PlayerWeaponHolder : WeaponHolder
    {
        [SerializeField] private PlayerMovement _player;
        
        protected override void Start()
        {
            base.Start();
            CurrentWeapon = WeaponList[0];
            _player.InputActions.Player.Shoot.started += OnShoot;
            _player.InputActions.Player.Shoot.canceled += OnShootCancel;
            _player.InputActions.Player.Reload.started += OnReload;
            _player.InputActions.Player.ChangeWeapon.started += OnChangeWeapon;
            _player.OnLookInput += ChangeWeaponDirection;
            _player.OnLookInput += OnSetDirection;
            SceneArchitect.OnPlayerDie += StopShoot;
        }

        private void StopShoot()
        {
            StopAttack();
        }

        private void OnSetDirection(Vector3 obj)
        {
            if(!_player.GetCanMove()) return;
            SetDirection(_player.LookInput);
        }

        private void OnShootCancel(InputAction.CallbackContext obj)
        {
            StopAttack();
        }


        private void OnChangeWeapon(InputAction.CallbackContext obj)
        {
            if(!_player.GetCanMove()) return;
            ChangeWeapon();
        }
        

        private void OnReload(InputAction.CallbackContext obj)
        {
            if(!_player.GetCanMove()) return;
            ReloadWeapon();
        }
        

        private void OnShoot(InputAction.CallbackContext context)
        {
            if(!_player.GetCanMove()) return;
            AttackWeapon(_player.LookInput);
            StartAttack();
            
        }
    }
}
