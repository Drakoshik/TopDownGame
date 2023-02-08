using System;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    [Serializable]
    public class WeaponClip
    {
        [SerializeField] private int _clip;
        [SerializeField] private float _reloadTime;
        private float _currentClip;

        // public WeaponClip(int clip, float reloadTime, float currentClip)
        // {
        //     _clip = clip;
        //     _reloadTime = reloadTime;
        //     _currentClip = currentClip;
        // }
        
        public void Reload()
        {
        
        }
    }
}
