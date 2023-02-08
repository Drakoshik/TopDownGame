using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace GameArchitecture.Weapon
{
    [Serializable]
    public class WeaponClip
    {
        public Action OnShoot;
        [SerializeField] private float _ammunition;
        [SerializeField] private float _clip;
        [SerializeField] private float _reloadTime;
        private float _currentClip;
        
        

        // public void Reload()
        // {
        //     StartCoroutine();
        // }

        public async Task Reload()
        {
            await Task.Delay((int)(_reloadTime * 1000));
            _ammunition -= _currentClip;
            _currentClip = _clip;
        }
    }
}
