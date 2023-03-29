using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource _audio;
    private Animator _animator;
    private Movement _movement;

    private DefaultControls _controls;
    private float _horizontalInput;
    private bool _isJumpPressed;
    private bool _jumpStop;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _controls = new DefaultControls();
        
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Main.Jump.performed += context => Jump(true);
        _controls.Main.Jump.canceled += context => Jump(false);

    }

    private void OnDisable()
    {
        _controls.Disable();
    }


    private void Update()
    {
        _horizontalInput = _controls.Main.Move.ReadValue<float>() * Time.fixedDeltaTime;

        if (!_audio.isPlaying && _horizontalInput != 0)
        {
            _audio.Play();
        }

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

}
