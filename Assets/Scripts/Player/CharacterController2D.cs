using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [SerializeField, Tooltip("Mandatory rigidbody that will be used for moving character")]                             Rigidbody2D _rigidbody;

    [Header("Air control")]
    [SerializeField, Tooltip("Maximum movement delta change when in air")] float maxAirTurnSpeed = 50f;

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
    [SerializeField, Tooltip("Horizontal velocity determining wall push impact when wall jumping")]                     float _wallJumpPushVelocity = 10f;

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
    /// Will ignore wall checking if wall jumping is not enabled
    /// </summary>
    public bool canJumpFromGroundOrWall { get; private set; }

    int _currentJumps;
    float _timeLeftToAllowJump;

    /// <summary>
    /// Move the character. 
    /// Should be called from FixedUpdate
    /// </summary>
    public void Move (bool isMovement, Vector2 movement, bool jump) {
        Vector2 targetVelocity = _rigidbody.velocity;

        // Update properties relating to state of player
        UpdateProperties(movement);

        // Movement logic
        MovementLogic(isMovement, movement, ref targetVelocity);

        // wall slide logic
        WallSlideLogic(ref targetVelocity);

        // Jump-related logic
        JumpLogic(jump, ref targetVelocity);

        // Flip (player) logic
        FlipLogic();

        // Apply target velocity at the end
        _rigidbody.velocity = targetVelocity;
    }

    void UpdateProperties (Vector2 movement) {
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

        canJumpFromGroundOrWall = isGrounded || (_enableWallJump && isHuggingWall);
    }

    void MovementLogic (bool isMovement, Vector2 movement, ref Vector2 targetVelocity) {
        if (isGrounded) {
            targetVelocity.x = movement.x;
        } else {
            if (isMovement) {
                targetVelocity.x = Mathf.MoveTowards(targetVelocity.x, movement.x, maxAirTurnSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void WallSlideLogic (ref Vector2 targetVelocity) {
        // Wall-slide logic
        if (_enableWallSlide) {
            if (isHuggingWall) {
                targetVelocity.y = Mathf.Clamp(targetVelocity.y, _minWallSlideGravityVelocity, float.MaxValue);
            }
        }
    }

    void JumpLogic (bool jump, ref Vector2 targetVelocity) {
        if (_timeLeftToAllowJump <= 0f) {
            // Reset current jumps when hitting ground / wall
            if (canJumpFromGroundOrWall) {
                _currentJumps = 0;
            }

            if (jump) {
                if (canJumpFromGroundOrWall || _currentJumps <= _extraAirJumps) {
                    // Jump
                    targetVelocity.y = jumpVelocity;

                    // Add wall push for wall jumping
                    if (_enableWallJump && isHuggingWall) {
                        targetVelocity.x = isFacingRight ? -_wallJumpPushVelocity : _wallJumpPushVelocity;
                    }

                    _timeLeftToAllowJump = _jumpResetTime;
                    _currentJumps++;
                }
            }
        } else {
            _timeLeftToAllowJump -= Time.fixedDeltaTime;
        }
    }

    void FlipLogic () {
        if (_flipIfChangingDirection) {
            // Flip player when facing another direction
            bool flipX = _startFacingRight ? !isFacingRight : isFacingRight;
            transform.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);   
        }
    }

    void Start () {
        isFacingRight = _startFacingRight;
    }

}
