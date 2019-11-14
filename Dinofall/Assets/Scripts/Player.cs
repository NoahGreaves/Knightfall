using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes sure the objects has the required components from the list below
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player : MonoBehaviour
{
    // values for the players movement
    [Header("Movement")]
    [SerializeField] private float _speed = 0f;
    [SerializeField] private float _acceleration = 0f;

    // values for checking for the gound
    [Header("Grounding")]
    [SerializeField] private float _groundCheckDistance = 0.125f;
    [SerializeField] private float _groundCheckOffset = 0.125f;
    [SerializeField] private LayerMask _groundCheckMask;

    // the values for the players falling air conrol
    [Header("Falling")]
    [SerializeField] private float _airControl = 0.1f;

    // unity properties
    protected Rigidbody2D Rigidbody {get; private set;}
    protected SpriteRenderer SpriteRenderer {get; private set;}
    protected Animator Animator {get; private set;}
    protected Collider2D Collider {get; private set;}
    
    // movement values
    private float _input;
    private bool _isTryingToMove;
    private bool _isGrounded;

    protected void Start()
    {
        // get components
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<Collider2D>();
    }

    private bool GroundCheck(float offset)
    {
        // create ground check origin position
        Vector2 horizontalOffset = Vector2.right * offset;
        Vector2 verticalOffset = Vector2.up * _groundCheckDistance * 0.5f;
        Vector2 origin = horizontalOffset + verticalOffset + (Vector2)transform.position;

        // check for ground using origin posisiton and ray casting
        if(Physics2D.Raycast(origin, Vector2.down * _groundCheckDistance, _groundCheckMask)) 
        {
            // ground found
            Debug.DrawRay(origin, Vector2.down * _groundCheckDistance, Color.blue);
            return true;
        }
        else
        {
            // ground not found
            Debug.DrawRay(origin, Vector2.down * _groundCheckDistance, Color.red);
            return false;
        }
    }

    private void Move(float input, float control) 
    {
        // calculate acceleration for desired velocity
        float targetVelocity = input * _speed;
        float currentVelocity = Rigidbody.velocity.x;
        float velocityDifference = targetVelocity - currentVelocity;
        float acceleration = velocityDifference * _acceleration * control;

        // move only on horizontal axis
        Rigidbody.AddForce(new Vector2(acceleration, 0f));
    }

    protected void Update()
    {
        // the input and movement of the player when the there is user input
        _input = Input.GetAxis("Horizontal");
        _isTryingToMove = Mathf.Abs(_input) >= 0.05f;

        // check if grounded first left then right
        bool grounded = GroundCheck(-_groundCheckOffset) || GroundCheck(_groundCheckOffset);

        Animator.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));

        // changes the sprites direction depending on the direction the player is moving
        if(_isTryingToMove)
        {
            SpriteRenderer.flipX = _input < 0f;
        }
    } 

    protected void FixedUpdate()
    {
        // move character on the ground
        if(_isGrounded)
        {
            // move or slow using full controll
            Move(_input, 1f);
        }
        // move character in the air
        else if(_isTryingToMove)
        {
            Move(_input, _airControl);
        }
    }
}
