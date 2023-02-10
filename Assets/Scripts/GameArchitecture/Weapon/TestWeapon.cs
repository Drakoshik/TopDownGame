using System;
using System.Collections;
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
            // _weaponClip.Reload();
        }

        private void OnEnable()
        {
            
        }

        public override void Attack(Vector2 direction)
        {
            StartCoroutine(Delay());
            throw new System.NotImplementedException();
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(AttackDelay);
        }
    }
}
