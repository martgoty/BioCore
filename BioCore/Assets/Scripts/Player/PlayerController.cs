using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAttackSystem))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    private AudioSource _audio;
    private Movement _movement;//скрипт для передвижения персонажа
    private DefaultControls _controls;//скрипт для управления
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PlayerAttackSystem _attackSystem;

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
        _controls = new DefaultControls();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attackSystem = GetComponent<PlayerAttackSystem>();


    }

    private void OnEnable()
    {
        //подключение управления
        _controls.Enable();
        _controls.Main.Jump.performed += context => Jump(true);//когда клавиша прыжка нажата
        _controls.Main.Jump.canceled += context => Jump(false);//когда клавиша прыжка отпущена
        _controls.Main.Attack.canceled += context => Attack();//когда кнопка атаки нажата

    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _horizontalInput = _controls.Main.Move.ReadValue<float>() * Time.fixedDeltaTime;
        _verticalInput = _controls.Main.Vertical.ReadValue<float>() * Time.fixedDeltaTime;

        Flip(_horizontalInput);

        if (!_audio.isPlaying && _horizontalInput != 0 && _movement.IsGrounded())
        {
            _audio.Play();
        }

        AnimationControl();

        AttackControl();
    }
    private void FixedUpdate()
    {
        _movement.PlayerMove(_horizontalInput, _isJumpPressed, _jumpStop);
        _isJumpPressed = false;
        _jumpStop = false;
    }

    private void Jump(bool isPressed)
    {
        if (isPressed)
        {
            _isJumpPressed = true;
        }
        else
        {
            _jumpStop = true;
        }
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

    private void Attack()
    {
        _attackSystem.Attack(_attackDir);
    }
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

