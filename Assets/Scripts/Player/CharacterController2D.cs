using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [SerializeField, Tooltip("Mandatory rigidbody that will be used for moving character")]                             Rigidbody2D _rigidbody;

    [Header("Ground / Wall Detection")]

    [SerializeField, Tooltip("Transform representing point that will be used as origin for ground check")]              Transform _groundCheckPoint;
    [SerializeField, Tooltip("Radius of circle for ground detection")]                                                  float _groundCheckRadius = 0.21f;
    [SerializeField, Tooltip("Layer that should be detected as ground")]                                                UnityEngine.LayerMask _groundCheckMask;

    [SerializeField, Tooltip("Transform representing point that will be used as origin for wall check")]                Transform _wallCheckPoint;
    [SerializeField, Tooltip("Radius of circle for wall detection")]                                                    float _wallCheckRadius = 0.35f;
    [SerializeField, Tooltip("Layer that should be detected as wall")]                                                  UnityEngine.LayerMask _wallCheckMask;

    [Header("Jumping")]

    [SerializeField, Tooltip("The height that each jump should reach")]                                                 float _jumpHeight = 2.5f;
    [SerializeField, Tooltip("Time after each jump where jumping again should not be allowed")]                         float _jumpResetTime = 0.25f;
    [SerializeField, Tooltip("Additional jumps that are allowed while in air")]                                         int _extraAirJumps = 1;

    [Header("Player direction")]

    [SerializeField, Tooltip("Should transform z scale be flipped when changing movement direction?")]                  bool _flipIfChangingDirection = true;
    [SerializeField, Tooltip("Does player sprite start facing right?")]                                                 bool _startFacingRight = true;

    [Header("Wall sliding / jumping")]

    [SerializeField, Tooltip("Should wall sliding be enabled?")]                                                        bool _enableWallSlide = true;
    [SerializeField, Tooltip("Minimum y velocity allowed when wall sliding. Used to prevent full force of gravity")]    float _minWallSlideGravityVelocity = -2f;
    [SerializeField, Tooltip("Should wall jumping be enabled?")]                                                        bool _enableWallJump = true;
    [SerializeField, Tooltip("")] float _wallJumpPushVelocity = 5f;
    [SerializeField, Tooltip("")] float _wallJumpPushTime = 0.5f;

    /// <summary>
    /// Velocity for player jumping. Calculated using jumpHeight
    /// </summary>
    public float jumpVelocity => Mathf.Sqrt(2 * -Physics2D.gravity.y * _rigidbody.gravityScale * _jumpHeight);

    /// <summary>
    /// Is the player on ground?
    /// </summary>
    public bool isGrounded { get; private set; }

    /// <summary>
    /// Is the player directly facing a wall?
    /// </summary>
    public bool isFacingWall { get; private set; }

    /// <summary>
    /// Is the player facing right?
    /// </summary>
    public bool isFacingRight { get; private set; }

    /// <summary>
    /// Is the player "hugging" the wall?
    /// In other words, is the player trying to move towards the wall it is facing?
    /// </summary>
    public bool isHuggingWall { get; private set; }

    /// <summary>
    /// Is the player on ground or hugging a wall?
    /// </summary>
    public bool canJumpFromGroundOrWall { get; private set; }

    Vector2 _lastMovement;
    Vector2 _lastWallPushVelocity;
    int _currentJumps;
    float _timeLeftToAllowJump;
    float _timeLeftToAllowMovement;

    /// <summary>
    /// Move the character. 
    /// Should be called from FixedUpdate
    /// </summary>
    public void Move (Vector2 movement, bool jump) {
        // Update properties relating to state of player
        isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundCheckMask) != null;
        isFacingWall = Physics2D.OverlapCircle(_wallCheckPoint.position, _wallCheckRadius, _wallCheckMask) != null;
        
        if (isFacingRight && movement.x < 0f) {
            isFacingRight = false;
        }

        if (!isFacingRight && movement.x > 0f) {
            isFacingRight = true;
        }

        // Set hugging wall to true if player is trying to move against wall
        if (isFacingWall) {
            isHuggingWall = (isFacingRight && movement.x > 0f) || (!isFacingRight && movement.x < 0f);
        } else {
            // Cannot be hugging wall if not facing wall
            isHuggingWall = false;
        }

        canJumpFromGroundOrWall = isGrounded || isHuggingWall;

        Vector2 velocityGravity = _rigidbody.velocity 
                                    - (_timeLeftToAllowMovement <= 0f ? _lastMovement : Vector2.zero) 
                                    - _lastWallPushVelocity;
        
        if (canJumpFromGroundOrWall && _timeLeftToAllowMovement <= 0f) {
            velocityGravity.x = 0f;
        }

        if (isGrounded) {
            _lastWallPushVelocity = Vector2.zero;
            _timeLeftToAllowMovement = 0f;
        }

        Vector2 targetVelocity = Vector2.zero;

        if (_timeLeftToAllowMovement <= 0f) {
            if (!isHuggingWall) {
                targetVelocity += movement;
                _lastMovement = movement;
            } else {
                _lastMovement = Vector2.zero;
            }

            _lastWallPushVelocity = Vector2.zero;
        } else {
            _lastMovement = Vector2.zero;
            _timeLeftToAllowMovement -= Time.fixedDeltaTime;
        }

        targetVelocity += velocityGravity + _lastWallPushVelocity;

        // Wall-slide logic
        if (_enableWallSlide) {
            if (isHuggingWall) {
                targetVelocity.y = Mathf.Clamp(targetVelocity.y, _minWallSlideGravityVelocity, float.MaxValue);
            }
        }

        // Jump-related logic
        if (_timeLeftToAllowJump <= 0f) {
            // Reset current jumps when hitting ground / wall
            if (canJumpFromGroundOrWall) {
                _currentJumps = 0;
            }

            if (jump) {
                if (canJumpFromGroundOrWall || _currentJumps <= _extraAirJumps) {
                    // Jump
                    targetVelocity.y = jumpVelocity;

                    if (isHuggingWall) {
                        _lastWallPushVelocity = new Vector2(isFacingRight ? -_wallJumpPushVelocity : _wallJumpPushVelocity, 0f);
                        _timeLeftToAllowMovement = _wallJumpPushTime;
                        targetVelocity.x = _lastWallPushVelocity.x;
                    }

                    _timeLeftToAllowJump = _jumpResetTime;
                    _currentJumps++;
                }
            }
        } else {
            _timeLeftToAllowJump -= Time.fixedDeltaTime;
        }

        if (_flipIfChangingDirection) {
            // Flip player when facing another direction
            bool flipX = _startFacingRight ? !isFacingRight : isFacingRight;
            transform.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);   
        }

        // Apply target velocity
        _rigidbody.velocity = targetVelocity;

        Debug.Log(_rigidbody.velocity);
    }

    void Start () {
        isFacingRight = _startFacingRight;
    }

}
