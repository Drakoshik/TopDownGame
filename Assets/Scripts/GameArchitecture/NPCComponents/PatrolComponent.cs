using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture.NPCComponents
{
    [Serializable]
    public enum PatrolState
    {
        Wait,
        Patrol
    }
    
    [Serializable]
    public class PatrolComponent
    {
        [SerializeField] private List<Transform> _moveSpots;
        [SerializeField] private float _speed;
        [SerializeField] private float _startWaitTime;

        private int _currentMoveSpotNumber = 0;
        private float _currentWaiteTime ;

        public PatrolState Patrol(Transform obj)
        {
            var currentMoveSpot = _moveSpots[_currentMoveSpotNumber];
            
            obj.position = Vector2.MoveTowards(obj.position,
                currentMoveSpot.position,
                _speed * Time.deltaTime);

            if (Vector2.Distance(obj.position, currentMoveSpot.position) < .2f)
            {
                if (_currentWaiteTime <= 0)
                {
                    _currentWaiteTime = _startWaitTime;
                    
                    if (_currentMoveSpotNumber >= _moveSpots.Count - 1)
                        _currentMoveSpotNumber = 0;
                    else 
                        _currentMoveSpotNumber += 1;
                }
                else
                {
                    _currentWaiteTime -= Time.deltaTime;
                    return PatrolState.Wait;
                }
            }
            
            return PatrolState.Patrol;
        }

        public void ResetPatrolTime()
        {
            _currentWaiteTime = _startWaitTime;
        }

    }
}