using System;
using GameArchitecture.Character;
using GameArchitecture.Character.PlayerClone;
using GameArchitecture.Weapon;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace GameArchitecture
{
    public class SceneArchitect : MonoBehaviour
    {
        [SerializeField] private GameObject[] _spawnPlaces;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private PlayerWeaponHolder _playerWeaponHolder;
        [SerializeField] private CloneManager _cloneManager;

        private void Start()
        {
            _playerWeaponHolder.CheckWeaponIndex();
            _player.transform.position = _spawnPlaces
                [Random.Range(0, _spawnPlaces.Length - 1)].transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ResetScene();
        }

        private void ResetScene()
        {
            _cloneManager.StartReplay();
            _playerWeaponHolder.CheckWeaponIndex();
            _player.transform.position = _spawnPlaces
                [Random.Range(0, _spawnPlaces.Length - 1)].transform.position;
        }
    }
}
