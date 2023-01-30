using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture.Character
{
    public class PlayerInputRecorder : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _player;

        private int _playerClonesAmount;

        private float _timer;
        
        private void Start()
        {
            _playerClonesAmount = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _timer = 0;
                PlayerReplayData.PlayerReplays.Add(new Dictionary<float, ReplayData>());
                print(PlayerReplayData.PlayerReplays.Count);
            }
        }

        private void FixedUpdate()
        {
            if(PlayerReplayData.PlayerReplays.Count <= 0) return;
            _timer += Time.deltaTime;
            PlayerReplayData.PlayerReplays[PlayerReplayData.PlayerReplays.Count-1].
                Add(_timer, new ReplayData(_player.MoveInput, _player.LookInput));
            print(_timer);
        }
    }
}
