using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAttackSystem))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _loading;
    private AudioSource _audio;
    private Movement _movement;//скрипт для передвижения персонажа
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerAttackSystem _attackSystem;
    private PlayerInput _input;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isJumpPressed;
    private bool _jumpStop;//контроль динамического прыжка
    private bool _isRight;

    private AttackDirectional _attackDir;
    public AttackDirectional AttackDir
    {
        get { return _attackDir; }
        set { _attackDir = value; }
    }//getter и setter

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackSystem = GetComponent<PlayerAttackSystem>();
        _input = GetComponent<PlayerInput>();
     
        Cursor.lockState = CursorLockMode.Locked;
    }
    #region input management
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isJumpPressed = true;
        }
        else
        {
            _jumpStop = true;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        _attackSystem.Attack(_attackDir);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<float>() * Time.fixedDeltaTime;
    }

    public void AttackDirection(InputAction.CallbackContext context)
    {
        _verticalInput = context.ReadValue<float>() * Time.fixedDeltaTime;
    }

    public void OpenChoiseMenu(InputAction.CallbackContext context)
    {
        if(context.performed){
            if(Time.timeScale >= 0.9f && !_loading.activeSelf){
                _input.SwitchCurrentActionMap("UI");
                GlobalEventsSystem.OpenMenu();
        }
        }


    }
    #endregion

    private void Update()
    {


        Flip(_horizontalInput);

        AnimationControl();
        AttackControl();

        if(_horizontalInput != 0 && _movement.IsGrounded() && !_audio.isPlaying)
        {
            _audio.Play();
        }

    }

    private void FixedUpdate()
    {
        _movement.PlayerMove(_horizontalInput, _isJumpPressed, _jumpStop);
        _isJumpPressed = false;
        _jumpStop = false;
    }

    private void AnimationControl()
    {
        _animator.SetBool("_isGrounded", _movement.IsGrounded());
        _animator.SetFloat("_velocityX", Mathf.Abs(_horizontalInput));
        _animator.SetFloat("_velocityY", _rigidbody2D.velocity.y);
    }

    public void Flip(float horizontal)
    {
        if (horizontal > 0f)
        {
            _isRight = true;
        }
        else if (horizontal < 0f)
        {
            _isRight = false;

        }
        _spriteRenderer.flipX = !_isRight;
    }

    /// <summary>
    /// Смена направления атаки
    /// </summary>
    private void AttackControl()
    {
        
        if (_verticalInput > 0)
        {
            _attackDir = AttackDirectional.Up;
        }
        else if (_verticalInput < 0)
        {
            _attackDir = AttackDirectional.Down;
        }
        else
        {
            if (_isRight)
            {
                _attackDir = AttackDirectional.Right;
            }
            else
            {
                _attackDir = AttackDirectional.Left;
            }
        }
    }
    public enum AttackDirectional
    {
        Left,
        Right,
        Up,
        Down,
    }
}

