using System;
using System.Collections.Generic;
using GameArchitecture.Pool;
using GameArchitecture.Weapon;
using UnityEngine;

namespace GameArchitecture.Character.PlayerClone
{
    public class CloneManager : MonoBehaviour
    {
        [SerializeField] private CloneEntity _clonePrefab;
        [SerializeField] private PlayerWeaponHolder _playerWeaponHolder;
        [SerializeField] private PlayerInputRecorder _playerInputRecorder;
        
        private Weapon.Weapon _currentWeapon;
        private List<Weapon.Weapon> _weaponList;

        private int _cloneNumber;
        private ObjectPool<CloneEntity> _clonePool;

        private void Start()
        {
            _cloneNumber = 0;
            _clonePool = new ObjectPool<CloneEntity>(_clonePrefab,
                3, true);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) StartReplay();
        }

        private void StartReplay()
        {
            _playerInputRecorder.StartRecord();
            _currentWeapon = _playerWeaponHolder.CurrentWeapon;
            _weaponList = _playerWeaponHolder.WeaponList;
            GetClone();
        }

        private void GetClone()
        {
            var clone = _clonePool.GetFreeElement();
            clone.SetUpClone(_cloneNumber, _currentWeapon, _weaponList);
            _cloneNumber++;
        }

        private void TakeAwayClones()
        {
            _clonePool.HideAllElements();
            _cloneNumber = 0;
        }
    }
}
