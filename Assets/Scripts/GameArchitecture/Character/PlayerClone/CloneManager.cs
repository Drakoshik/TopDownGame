using System;
using GameArchitecture.Pool;
using UnityEngine;

namespace GameArchitecture.Character.PlayerClone
{
    public class CloneManager : MonoBehaviour
    {
        // public Weapon.Weapon CurrentWeapon { get; private set;}
        // public List<Weapon.Weapon> WeaponList { get; private set;}

        [SerializeField] private CloneEntity _clonePrefab;

        private ObjectPool<CloneEntity> _clonePool;

        private void Start()
        {
            _clonePool = new ObjectPool<CloneEntity>(_clonePrefab,
                3, true);
        }
    }
}
