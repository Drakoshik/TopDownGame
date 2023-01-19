using System;
using System.Collections.Generic;
using GameArchitecture.Actions;
using GameArchitecture.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameArchitecture.Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _rotateHolder;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private List<Weapon> _weaponList;

        private bool _canRotate = true;
        
        private void Start()
        {
            _currentWeapon = _weaponList[0];
            _player.InputActions.Player.Shoot.started += OnShoot;
            _player.InputActions.Player.Reload.started += OnReload;
            _player.InputActions.Player.ChangeWeapon.started += OnChangeWeapon;
            _player.OnLookInput += ChangeWeaponDirection;
            ChangeCanRotateHolder.OnCanRotate += CanRotate;
        }

        private void OnChangeWeapon(InputAction.CallbackContext obj)
        {
            if(!_canRotate) return;
            if (_currentWeapon == _weaponList[0])
            {
                _currentWeapon = _weaponList[1];
                _weaponList[1].gameObject.SetActive(true);
                _weaponList[0].gameObject.SetActive(false);
            }
            else
            {
                _currentWeapon = _weaponList[0];
                _weaponList[0].gameObject.SetActive(true);
                _weaponList[1].gameObject.SetActive(false);
            }
        }

        private void OnReload(InputAction.CallbackContext obj)
        {
            if (_currentWeapon is RealoadableWeapon weapon)
            {
                weapon.Reload();
            }
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            _currentWeapon.Attack(_player.LookInput);
        }

        private void ChangeWeaponDirection(Vector3 direction)
        {
            if(direction == Vector3.zero) return;
            if(!_canRotate) return;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _currentWeapon.Flip(MathF.Abs(angle) >= 90);
        }

        private void CanRotate(bool isCan)
        {
            _canRotate = isCan;
        }
    }
}
