using System;
using GameArchitecture.Character;
using GameArchitecture.Character.PlayerClone;
using GameArchitecture.View;
using GameArchitecture.Weapon;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace GameArchitecture
{
    public class SceneArchitect : MonoBehaviour
    {
        public static SceneArchitect Instance;
        public float GlobalTimer { get; private set; }
        
        [SerializeField] private GameObject[] _spawnPlaces;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private PlayerWeaponHolder _playerWeaponHolder;
        [SerializeField] private CloneManager _cloneManager;

        [SerializeField] private TimerCanvas _timerCanvas;
        [SerializeField] private GameObject[] _levels;

        private float _timeForLevel = 120;
        private int _level;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }


        private void Start()
        {
            _playerWeaponHolder.CheckWeaponIndex();
            _player.transform.position = _spawnPlaces
                [Random.Range(0, _spawnPlaces.Length - 1)].transform.position;
            _timerCanvas.ResetSlider(_timeForLevel);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ResetScene();
        }

        private void FixedUpdate()
        {
            GlobalTimer += Time.deltaTime;
            if (GlobalTimer >= _timeForLevel)
            {
                _level++;
                ResetScene();
            }
        }

        public float GetTimerPercent()
        {
            return 100 * GlobalTimer / _timeForLevel;
        }


        private void ResetScene()
        {
            _timerCanvas.ResetSlider(_timeForLevel);
            GlobalTimer = 0;
            _cloneManager.StartReplay();
            _playerWeaponHolder.CheckWeaponIndex();
            _player.transform.position = _spawnPlaces
                [Random.Range(0, _spawnPlaces.Length - 1)].transform.position;
        }
    }
}
