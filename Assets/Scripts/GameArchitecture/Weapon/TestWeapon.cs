using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    [Serializable]
    public class TestWeapon : Weapon
    {
        [SerializeField] private WeaponClip _weaponClip;

        public override void Start()
        {
            base.Start();
            // _weaponClip = new WeaponClip();
        }

        public override void Attack(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

    
    }
}
