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
        }

        private void OnSetDirection(Vector3 obj)
        {
            SetDirection(_player.LookInput);
        }

        private void OnShootCancel(InputAction.CallbackContext obj)
        {
            StopAutoAttack();
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
            StartAutoAttack();
            
        }
        
        // public void ClickHoldRelease(InputAction.CallbackContext context)
        // {
        //     System.Type vector2Type = Vector2.zero.GetType();
        //
        //     string buttonControlPath = "/Mouse/leftButton";
        //     //string deltaControlPath = "/Mouse/delta";
        //
        //     Debug.Log(context.control.path);
        //     //Debug.Log(context.valueType);
        //
        //     if (context.started)
        //     {
        //         if (context.control.path == buttonControlPath)
        //             //if (context.valueType != vector2Type)
        //         {
        //             Debug.Log("Button Pressed Down Event - called once when button pressed");
        //         }
        //     }
        //     else if (context.performed)
        //     {
        //         if (context.control.path == buttonControlPath)
        //             //if (context.valueType != vector2Type)
        //         {
        //             Debug.Log("Button Hold Down - called continously till the button is pressed");
        //         }
        //     }
        //     else if (context.canceled)
        //     {
        //         if (context.control.path == buttonControlPath)
        //             //if (context.valueType != vector2Type)
        //         {
        //             Debug.Log("Button released");
        //         }
        //     }
        // }
    }
}
