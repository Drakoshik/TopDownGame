using System;
using System.Collections;
using GameArchitecture.Actions;
using GameArchitecture.Interfaces.ForDealDamage;
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
        private bool _isDashing;

        private Rigidbody2D _rigidbody2D;
    
        private static readonly int LastX = Animator.StringToHash("LastX");
        private static readonly int LastY = Animator.StringToHash("LastY");
        private static readonly int MovementX = Animator.StringToHash("MovementX");
        private static readonly int MovementY = Animator.StringToHash("MovementY");
        private static readonly int DashAnimation = Animator.StringToHash("Dash");

        private void Awake()
        {
            InputActions = new PlayerInputActions();
            PlayerActions = InputActions.Player;
            _playerAnimator = GetComponent<Animator>();
            PlayerActions.Dash.started += Dash;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Dash(InputAction.CallbackContext obj)
        {
            if (_movementInput == Vector2.zero) return;
            if(_isDashing) return;
            StartCoroutine(StartDash());
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
            ReadLookInput();
            if (_isDashing)
            {
                DashMove();
                return;
            }
            ReadMovementInput();
            Move();
        }

        private void DashMove()
        {
            _rigidbody2D.velocity = new Vector2(_dashDirection.x * 15,
                _dashDirection.y * 15);  
        }

        private void ReadMovementInput()
        {
            _movementInput = PlayerActions.Movement.ReadValue<Vector2>();
            _rigidbody2D.velocity = Vector2.zero;
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
            if (col.GetComponent<IDealsDamageToPlayer>() != null)
            {
                // SceneArchitect.OnPlayerDie?.Invoke();
                // _canMove = false;
                Debug.Log("Player died!");
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

        private IEnumerator StartDash()
        {
            _invulnerability = true;
            _isDashing = true;
            _playerAnimator.SetBool(DashAnimation, true);
            yield return new WaitForSeconds(_invulTime);
            _invulnerability = false;
            _playerAnimator.SetBool(DashAnimation, false);
            yield return new WaitForSeconds(.2f);
            _isDashing = false;
        }
    }
}
