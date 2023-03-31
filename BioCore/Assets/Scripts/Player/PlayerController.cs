using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource _audio;
    private Animator _animator;
    private Movement _movement;
    private DefaultControls _controls;//скрипт для управления
    private PlayerAttack _attackControll;//скрипт для создания точки атаки

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isJumpPressed;
    private bool _jumpStop;//контроль динамического прыжка
    private PlayerAttack.AttackDirections _lastAttackDir;//переменная для возвращения напрвления атаки в исходное состояние

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _controls = new DefaultControls();
        _attackControll = GetComponent<PlayerAttack>();
        
    }

    private void OnEnable()
    {
        //подключение управления
        _controls.Enable();
        _controls.Main.Jump.performed += context => Jump(true);//когда клавиша прыжка нажата
        _controls.Main.Jump.canceled += context => Jump(false);//когда клавиша прыжка отпущена

    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        if (_movement._isRight)
        {
            _lastAttackDir = PlayerAttack.AttackDirections.Right;
        }
        else
        {
            _lastAttackDir = PlayerAttack.AttackDirections.Left;
        }
    }

    private void Update()
    {
        _horizontalInput = _controls.Main.Move.ReadValue<float>() * Time.fixedDeltaTime;
        _verticalInput = _controls.Main.Vertical.ReadValue<float>() * Time.fixedDeltaTime;

        if (!_audio.isPlaying && _horizontalInput != 0)
        {
            _audio.Play();
        }

        ChangeAttackDirection();

        AnimationsControl();

    }
    private void FixedUpdate()
    {
        _movement.Flip(_horizontalInput);
        _movement.PlayerMove(_horizontalInput, _isJumpPressed, _jumpStop);
        _isJumpPressed = false;
        _jumpStop = false;

    }

    private void AnimationsControl()
    {
        _animator.SetFloat("_velocityX", Mathf.Abs(_horizontalInput));
        _animator.SetFloat("_velocityY", _rb.velocity.y);
        _animator.SetBool("_isGrounded", _movement.IsGrounded());
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

    private void ChangeAttackDirection()
    {
        if(_verticalInput > 0)
        {
            _attackControll.attackState = PlayerAttack.AttackDirections.Up;
        }
        else if(_verticalInput < 0)
        {
            _attackControll.attackState = PlayerAttack.AttackDirections.Down;
        }
        else
        {
           
            if(_horizontalInput < 0)
            {
                _attackControll.attackState = PlayerAttack.AttackDirections.Left;
                _lastAttackDir = PlayerAttack.AttackDirections.Left;

            }
            else if(_horizontalInput > 0)
            {
                _attackControll.attackState = PlayerAttack.AttackDirections.Right;
                _lastAttackDir = PlayerAttack.AttackDirections.Right;

            }
            else
            {
                _attackControll.attackState = _lastAttackDir;
            }
        }
    }

}
