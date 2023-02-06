using System;
using System.Collections.Generic;
using GameArchitecture.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameArchitecture.Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        [field: SerializeField] public Weapon CurrentWeapon { get; protected set; }
        [field: SerializeField] public List<Weapon> WeaponList{ get; protected set; }
        
        [field: SerializeField] public GameObject Holder { get; protected set; }
        [field: SerializeField] public int CurrentWeaponIndex { get; private set; }
        
        private bool _canRotate = true;

        protected virtual void Start()
        {
            ChangeCanRotateHolder.OnCanRotate += CanRotate;
        }

        protected void ChangeWeapon()
        {
            if (!_canRotate) return;
            if(WeaponList.Count <= 1) return;
            if (CurrentWeapon == WeaponList[0])
            {
                CurrentWeapon = WeaponList[1];
                WeaponList[1].gameObject.SetActive(true);
                WeaponList[0].gameObject.SetActive(false);
            }
            else
            {
                CurrentWeapon = WeaponList[0];
                WeaponList[0].gameObject.SetActive(true);
                WeaponList[1].gameObject.SetActive(false);
            }
        }
        
        protected void ReloadWeapon()
        {
            if (CurrentWeapon is RealoadableWeapon weapon)
            {
                weapon.Reload();
            }
        }
        protected void StartAttack()
        {
            if (CurrentWeapon is SubmachineGun weapon)
            {
                weapon.StartAuto();
            }
        }
        protected void StopAttack()
        {
            if (CurrentWeapon is SubmachineGun weapon)
            {
                weapon.StopAuto();
            }
        }
        
        protected void SetDirection(Vector3 direction)
        {
            if (CurrentWeapon is SubmachineGun weapon)
            {
                weapon.SetDirection(direction);
            }
        }
        
        protected void AttackWeapon(Vector3 direction)
        {
            CurrentWeapon.Attack(direction);
        }
        
        protected void ChangeWeaponDirection(Vector3 direction)
        {
            if(direction == Vector3.zero) return;
            if(!_canRotate) return;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            CurrentWeapon.Flip(MathF.Abs(angle) >= 90);
        }
        
        private void CanRotate(bool isCan)
        {
            _canRotate = isCan;
        }

        public void CheckWeaponIndex()
        {
            for (var i = 0; i < WeaponList.Count; i++)
            {
                if (CurrentWeapon == WeaponList[i])
                {
                    CurrentWeaponIndex = i;
                    return;
                }
            }

            CurrentWeaponIndex = 0;
        }
        
        
    }
}
