using System;
using System.Collections.Generic;
using GameArchitecture.Character;
using GameArchitecture.Character.PlayerClone;
using GameArchitecture.View;
using GameArchitecture.Weapon;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR;
using Random = UnityEngine.Random;

namespace GameArchitecture
{

    public enum GameState
    {
        GameStart,
        GameReset,
        NextLevelStart
    }
    public class SceneArchitect : MonoBehaviour
    {
        public static SceneArchitect Instance;
        public static Action OnPlayerDie;
        public float GlobalTimer { get; private set; }
        
        [SerializeField] private GameObject[] _spawnPlaces;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private PlayerInputRecorder _playerInputRecorder;
        [SerializeField] private PlayerWeaponHolder _playerWeaponHolder;
        [SerializeField] private CloneManager _cloneManager;

        [SerializeField] private TimerCanvas _timerCanvas;
        [SerializeField] private UICanvas _uiCanvas;
        [SerializeField] private GameObject[] _levels;

        [SerializeField] private float _timeForLevel = 60;

        [SerializeField] private List<Transform> _targetList;
        private int _level;

        private bool _canInput = true;
        private bool _isTimeEnd = false;

        private GameState _currentState;
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
            _targetList = new List<Transform>();
            _targetList.Add(_player.transform);
            _currentState = GameState.GameStart;
           OnPlayerDie += ResetLevel;
           _uiCanvas.ShowTitle();
        }

        private void ResetLevel()
        {
            _uiCanvas.ShowBackInTime();
            _canInput = true;
            _levels[_level].SetActive(false);
            _currentState = GameState.GameReset;
        }

        private void Update()
        {
            if(!_canInput) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (_currentState)
                {
                    case GameState.GameStart:
                        _level = 0;
                        _playerInputRecorder.StartRecord();
                        _uiCanvas.HideTitle();
                        break;
                    case GameState.GameReset:
                        _cloneManager.StartReplay();
                        _uiCanvas.HideBackInTime();
                        break;
                    case GameState.NextLevelStart:
                        _cloneManager.TakeAwayClones();
                        _uiCanvas.HideBackInTime();
                        break;
                }
                ResetScene();
                _canInput = false;
                _isTimeEnd = false; 
                
            }
        }

        public GameState GetCurrentGameState()
        {
            return _currentState;
        }

        private void FixedUpdate()
        {
            GlobalTimer += Time.deltaTime;
            if(_isTimeEnd) return;
            if (GlobalTimer >= _timeForLevel)
            {
                _levels[_level].SetActive(false);
                _level++;
                if (_level > 2) _level = 0;
                _canInput = true;
                _currentState = GameState.NextLevelStart;
                _isTimeEnd = true;
                _uiCanvas.ShowBackInTime();
                _cloneManager.TakeAwayClones();
            }
        }

        private void ResetPlayer()
        {
            _player.gameObject.SetActive(false);
            _player.gameObject.SetActive(true);
            _playerWeaponHolder.CheckWeaponIndex();
            _player.transform.position = _spawnPlaces
                [Random.Range(0, _spawnPlaces.Length - 1)].transform.position;
        }

        public float GetTimerPercent()
        {
            return 100 * GlobalTimer / _timeForLevel;
        }


        private void ResetScene()
        {
            _timerCanvas.ResetSlider(_timeForLevel);
            GlobalTimer = 0;
            _levels[_level].SetActive(false);
            _levels[_level].SetActive(true);
            
            ResetPlayer();
        }

        public Transform GetCurrentTarget()
        {
            return _targetList[_targetList.Count - 1];
        }

        public void AddNewTarget(Transform newTarget)
        {
            _targetList.Add(newTarget);
        }
        
        public void RemoveTarget(Transform target)
        {
            _targetList.Remove(target);
        }
    }
}
