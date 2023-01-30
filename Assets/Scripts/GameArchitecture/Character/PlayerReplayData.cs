using System.Collections.Generic;
using UnityEngine;

namespace GameArchitecture.Character
{

    public struct ReplayData
    {
        public Vector2 MovementInput { get; private set;}
        public Vector2 LookInput { get; private set;}
        public bool IsShoot { get; private set;}
        public bool IsReload { get; private set;}
        public bool IsChangeWeapon { get; private set;}



        public ReplayData(Vector2 movementInput, Vector2 lookInput,
            bool isShoot = false, bool isReload= false, bool isChangeWeapon= false)
        {
            MovementInput = movementInput;
            LookInput = lookInput;
            IsShoot = isShoot;
            IsReload = isReload;
            IsChangeWeapon = isChangeWeapon;
        }
        
    }
    
    public static class PlayerReplayData
    {
        public static List<Dictionary<float,ReplayData>> PlayerReplays =
            new List<Dictionary<float, ReplayData>>();

        private static void SetReplayData(ReplayData replayData)
        {
            
        }

    }
}
