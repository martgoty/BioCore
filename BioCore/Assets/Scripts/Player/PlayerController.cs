using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private AudioSource _audio;
    private Animator _animator;
    private Movement _movement;

    private float horizontalInput;
    private float verticalInput;
    private bool isJumpPressed;
    private bool jumpStop;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
    }



    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        verticalInput = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;

        if (!_audio.isPlaying && horizontalInput != 0)
        {
            _audio.Play();
        }

        AnimationsControl();

        ButtonsClickCheck();

    }
    private void FixedUpdate()
    {
        _movement.Flip(horizontalInput);
        _movement.PlayerMove(horizontalInput, isJumpPressed, jumpStop);
        isJumpPressed = false;
        jumpStop = false;

    }



    private void AnimationsControl()
    {
        _animator.SetFloat("_velocityX", Mathf.Abs(horizontalInput));
        _animator.SetFloat("_velocityY", _rb.velocity.y);
        _animator.SetBool("_isGrounded", _movement.IsGrounded());
    }

    private void ButtonsClickCheck()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumpPressed = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpStop = true;
        }
    }

}
