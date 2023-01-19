using System;
using GameArchitecture.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameArchitecture
{
    public class InputDeviceChecker : MonoBehaviour
    {
        private InputDevice _inputDevice;
        private InputDevice _lastInputDevice;
        private void Awake()
        {
            InputSystem.onActionChange += (obj, change) =>
            {
                if (change != InputActionChange.ActionPerformed) return;
                if (obj is InputAction action) 
                    if(action.activeControl.device == _inputDevice)
                        return;
                _inputDevice = (obj as InputAction)?.activeControl.device;
                UpdateDeviceType();
            };
        }
    
 
        private void UpdateDeviceType()
        {
            switch (_inputDevice)
            {
                case Keyboard or Mouse:
                    if(_lastInputDevice is Keyboard or Mouse) return;
                    _lastInputDevice = _inputDevice;
                    break;
                case Gamepad:
                    _lastInputDevice = _inputDevice;
                    break;
                case Touchscreen:
                    throw new Exception();
                case Joystick:
                    throw new Exception();
                default:
                    throw new Exception();
            }
        
            InputChangeAction.OnInputChange?.Invoke(_lastInputDevice);
        }
    }
}
