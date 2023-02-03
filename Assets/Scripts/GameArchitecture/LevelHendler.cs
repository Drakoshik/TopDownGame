using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture
{
    public class LevelHendler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemys = new List<GameObject>();

        private SceneArchitect _sceneArchitect;
        private int _currentObjectNumber;
        private float _percentObjectStep;
        private float _currentPercentObjectStep;

        private void Start()
        {
            _sceneArchitect = SceneArchitect.Instance;
        }

        private void OnEnable()
        {
            _currentObjectNumber = 0;
            _percentObjectStep = 100 / _enemys.Count;
            _currentPercentObjectStep = 0;
            foreach (var enemy in _enemys)
            {
                enemy.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            if (_sceneArchitect.GetTimerPercent() > _currentPercentObjectStep)
            {
                if(_currentObjectNumber >= _enemys.Count) return;
                _enemys[_currentObjectNumber].SetActive(true);
                _currentPercentObjectStep += _percentObjectStep;
                _currentObjectNumber++;
            }
        }
    }
}
