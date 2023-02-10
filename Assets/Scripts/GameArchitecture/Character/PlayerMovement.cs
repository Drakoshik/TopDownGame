using System;
using System.Collections;
using GameArchitecture.Actions;
using GameArchitecture.Weapon.Bullets;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GameArchitecture.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 5f;

        [SerializeField] private GameObject crossHair;
        [SerializeField] private float _invulTime;
        public PlayerInputActions InputActions { get; private set; }
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        public event Action<Vector3> OnLookInput; 
        public Vector2 LookInput { get; private set; }
        public Vector2 MoveInput { get; private set; }

        private InputDevice _currentDevice;
    
        private Vector2 _movementInput;

        private Camera _mainCamera;

        private Animator _playerAnimator;

        private bool _canMove = true;

        private bool _invulnerability;

        private Vector2 _dashDirection;
    
        private static readonly int LastX = Animator.StringToHash("LastX");
        private static readonly int LastY = Animator.StringToHash("LastY");
        private static readonly int MovementX = Animator.StringToHash("MovementX");
        private static readonly int MovementY = Animator.StringToHash("MovementY");

        private void Awake()
        {
            InputActions = new PlayerInputActions();
            PlayerActions = InputActions.Player;
            _playerAnimator = GetComponent<Animator>();
            PlayerActions.Dash.started += Dash;
        }

        private void Dash(InputAction.CallbackContext obj)
        {
            StartCoroutine(StartInvulFrames());
            _dashDirection = _movementInput.normalized;
            
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            _invulnerability = false;
        }

        private void FixedUpdate()
        {
            if(!_canMove) return;
            ReadMovementInput();
            Move();
            ReadLookInput();
        }
        
        private void ReadMovementInput()
        {
            _movementInput = PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void ReadLookInput()
        {
            LookInput = PlayerActions.GamepadLook.ReadValue<Vector2>();
            if (_currentDevice is (Keyboard or Mouse))
            {
                LookInput = PlayerActions.MouseLook.ReadValue<Vector2>();
                Vector3 mousePosition = PlayerActions.MouseLook.ReadValue<Vector2>();

                LookInput = mousePosition -
                            _mainCamera.WorldToScreenPoint(transform.position);

                crossHair.transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(mousePosition);
            }
            if(LookInput == Vector2.zero) return;
            _playerAnimator.SetFloat(LastX, LookInput.x);
            _playerAnimator.SetFloat(LastY, LookInput.y);
            LookInput = LookInput.normalized;
            OnLookInput?.Invoke(LookInput);
        

        }

        private void Move()
        {
            if (_movementInput == Vector2.zero)
            {
                _playerAnimator.SetFloat(MovementX, _movementInput.x);
                _playerAnimator.SetFloat(MovementY, _movementInput.y);
                MoveInput = transform.position;
                return;
            }

            transform.position += new Vector3(_movementInput.x * speedMultiplier,
                _movementInput.y * speedMultiplier,
                0f) * Time.deltaTime ;
            MoveInput = transform.position;
        
            _playerAnimator.SetFloat(MovementX, _movementInput.x);
            _playerAnimator.SetFloat(MovementY, _movementInput.y);
        }

        private void OnDeviceChange(InputDevice device)
        {
            _currentDevice = device;
            crossHair.SetActive(_currentDevice is (Keyboard or Mouse));
        }
    
        private void OnEnable()
        {
            _canMove = true;
            InputActions.Enable();
            InputChangeAction.OnInputChange += OnDeviceChange;
            
        }

        private void OnDisable()
        {
            InputActions.Disable();
            InputChangeAction.OnInputChange -= OnDeviceChange;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(_invulnerability) return;
            if (col.GetComponent<Enemy.Enemy>() || col.GetComponent<EnemyProjectile>())
            {
                SceneArchitect.OnPlayerDie?.Invoke();
                _canMove = false;
            }
        }

        public Vector2 GetMovementInput()
        {
            return _movementInput;
        }

        public bool GetCanMove()
        {
            return _canMove;
        }

        private IEnumerator StartInvulFrames()
        {
            _invulnerability = true;
            yield return new WaitForSeconds(_invulTime);
            _invulnerability = false;
        }
    }
}
