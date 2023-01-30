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
            _player.InputActions.Player.Reload.started += OnReload;
            _player.InputActions.Player.ChangeWeapon.started += OnChangeWeapon;
            _player.OnLookInput += ChangeWeaponDirection;
        }
        

        private void OnChangeWeapon(InputAction.CallbackContext obj)
        {
            ChangeWeapon();
        }
        

        private void OnReload(InputAction.CallbackContext obj)
        {
            ReloadWeapon();
        }
        

        private void OnShoot(InputAction.CallbackContext context)
        {
            AttackWeapon(_player.LookInput);
        }
        
        
    }
}
