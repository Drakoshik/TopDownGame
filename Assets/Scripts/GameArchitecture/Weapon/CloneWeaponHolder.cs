using System.Collections.Generic;
using GameArchitecture.Character.PlayerClone;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    public class CloneWeaponHolder : WeaponHolder
    {
        private CloneController _cloneController;
        
        protected override void Start()
        {
            base.Start();
            
            _cloneController.OnShoot += OnShoot;
            _cloneController.OnShootCancel += OnShootCancel;
            _cloneController.OnReload += OnReload;
            _cloneController.OnChangeWeapon += OnChangeWeapon;
            _cloneController.OnLook += ChangeWeaponDirection;
            _cloneController.OnLook += OnSetDirection;
        }

        public void SetUpCloneWeaponHolder(CloneController cloneController,
            int weaponIndex)
        {
            _cloneController = cloneController;
            for (int i = 0; i < WeaponList.Count; i++)
            {
                if(i != weaponIndex) WeaponList[i].gameObject.SetActive(false);
            }
            CurrentWeapon = WeaponList[weaponIndex];
            CurrentWeapon.gameObject.SetActive(true);
        }
        private void OnSetDirection(Vector3 direction)
        {
            SetDirection(direction);
        }

        private void OnShootCancel()
        {
            StopAttack();
        }

        private void OnChangeWeapon()
        {
            ChangeWeapon();
        }
        

        private void OnReload()
        {
            ReloadWeapon();
        }
        

        private void OnShoot(Vector3 direction)
        {
            AttackWeapon(direction);
            StartAttack();
        }
    }
}
