using System;
using System.Collections.Generic;
using GameArchitecture.Weapon;
using UnityEngine;

namespace GameArchitecture.Character.PlayerClone
{
    public class CloneEntity : MonoBehaviour
    {
        [SerializeField] private CloneController _cloneController;
        [SerializeField] private CloneWeaponHolder _cloneWeaponHolder;
        
        public void SetUpClone(int cloneNumber, Weapon.Weapon currentWeapon,
            List<Weapon.Weapon> weaponList)
        {
            _cloneController.SetCloneNumber(cloneNumber);
            _cloneWeaponHolder.SetUpCloneWeaponHolder(_cloneController,
                currentWeapon, weaponList);
        }
    }
}
