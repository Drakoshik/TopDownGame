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
            _cloneController.OnReload += OnReload;
            _cloneController.OnChangeWeapon += OnChangeWeapon;
            _cloneController.OnLook += ChangeWeaponDirection;
        }

        public void SetUpCloneWeaponHolder(CloneController cloneController,
            Weapon currentWeaponSet, List<Weapon> weaponListSet)
        {
            _cloneController = cloneController;
            CurrentWeapon = currentWeaponSet;
            WeaponList = weaponListSet;
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
        }
    }
}
