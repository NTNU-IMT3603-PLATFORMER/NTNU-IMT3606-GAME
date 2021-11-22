using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityCollision))]
public class CharacterController2D : MonoBehaviour {

    [SerializeField, Tooltip("Mandatory rigidbody that will be used for moving character")]                             Rigidbody2D _rigidbody;

    [Header("Air control")]
    [SerializeField, Tooltip("Maximum movement delta change when in air")]                                              float maxAirTurnSpeed = 50f;

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

    [Header("Dashing")]
    [SerializeField, Tooltip("Should dashing be enabled?")]                                                             bool _enableDashing = true;
    [SerializeField, Tooltip("How far should the player dash?")]                                                        float _dashDistance = 4f;
    [SerializeField, Tooltip("How fast should the player dash?")]                                                       float _dashSpeed = 20f;
    [SerializeField, Tooltip("Additional jumps that are allowed while in air")]                                         int _maxDashes = 1;
    [SerializeField, Tooltip("The time it takes to allow dashing again")]                                               float _dashResetTime = 1f;

    /// <summary>
    /// Rigidbody used for moving the character
    /// </summary>
    public new Rigidbody2D rigidbody => _rigidbody;

    /// <summary>
    /// Velocity for player jumping. Calculated using jumpHeight
    /// </summary>
    public float jumpVelocity => Mathf.Sqrt(2 * -Physics2D.gravity.y * _rigidbody.gravityScale * _jumpHeight);

    /// <summary>
    /// Should wall sliding be enabled?
    /// </summary>
    public bool enableWallSlide {
        get => _enableWallSlide;
        set => _enableWallSlide = value;
    }

    /// <summary>
    /// Should wall jumping be enabled?
    /// </summary>
    public bool enableWallJump {
        get => _enableWallJump;
        set => _enableWallJump = value;
    }
    /// <summary>
    /// The height that each jump should reach
    /// </summary>
    public float jumpHeight {
        get => _jumpHeight;
        set => _jumpHeight = value;
    }

    /// <summary>
    /// Additional jumps that are allowed while in air
    /// </summary>
    public int extraAirJumps {
        get => _extraAirJumps;
        set => _extraAirJumps = value;
    }

    /// <summary>
    /// Should dashing be enabled?
    /// </summary>
    public bool enableDashing {
        get => _enableDashing;
        set => _enableDashing = value;
    }

    /// <summary>
    /// How far should the player dash?
    /// </summary>
    public float dashDistance {
        get => _dashDistance;
        set => _dashDistance = value;
    }

    /// <summary>
    /// How fast should the player dash?
    /// </summary>
    public float dashSpeed {
        get => _dashSpeed;
        set => _dashSpeed = value;
    }

    /// <summary>
    /// Additional jumps that are allowed while in air
    /// </summary>
    public int maxDashes {
        get => _maxDashes;
        set => _maxDashes = value;
    }


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
    /// Gets a normalized vector representing the direction player is facing
    /// </summary>
    public Vector2 playerDirection => isFacingRight ? new Vector2(1f, 0f) : new Vector2(-1f, 0f);

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

    /// <summary>
    /// The amount of jumps player has performed.
    /// Will reset when hitting ground or wall
    /// (if wall jumping is enabled)
    /// </summary>
    public int currentJumps { get; private set; }
    
    /// <summary>
    /// Is the player currently dashing (mid-dash)?
    /// </summary>
    public bool isDashing { get; private set; }

    /// <summary>
    /// The amount of dashes player has performed.
    /// Will reset when hitting ground or wall
    /// (if wall jumping is enabled)
    /// </summary>
    public int currentDashes { get; private set; }

    /// <summary>
    /// Check if the Character has been hit recently, used to stop movement to make knockback possible.
    /// </summary>
    public bool isHit { get; set; }


    public UnityEvent<Collider2D> eventOnGrounded => _eventOnGrounded;
    public UnityEvent eventOnLeftGround => _eventOnLeftGround;
    public UnityEvent eventOnJump => _eventOnJump;

    public Rigidbody2D movingPlatformRigidbody { get; set; }

    float _timeLeftToAllowJump;
    float _timeLeftToAllowDash;
    float _dashDistanceLeft;

    Vector2 _lastVelocity;
    float _gravityScaleBeforeDash;
    Vector3 _lastParentCoordinates;
    Collider2D[] _childColliders;

    UnityEvent<Collider2D> _eventOnGrounded = new UnityEvent<Collider2D>();
    UnityEvent _eventOnLeftGround = new UnityEvent();
    UnityEvent _eventOnJump = new UnityEvent();

    /// <summary>
    /// Move the character. 
    /// Should be called from FixedUpdate
    /// </summary>
    public void Move (bool isMovement, Vector2 movement, bool jump, bool dash) {
        if(isHit) {
            return;
        }
        Vector2 targetVelocity = _rigidbody.velocity;

        // Update properties relating to state of player
        UpdateProperties(movement);

        // Movement logic
        MovementLogic(isMovement, movement, ref targetVelocity);

        // wall slide logic
        WallSlideLogic(ref targetVelocity);

        // Jump-related logic
        JumpLogic(jump, ref targetVelocity);

        // Dash logic
        DashLogic(dash, ref targetVelocity);

        // Flip (player) logic
        FlipLogic();

        // Add moving platform velocity if on a platform
        targetVelocity += new Vector2(movingPlatformRigidbody?.velocity.x ?? 0f, 0f);

        // Apply target velocity at the end
        _rigidbody.velocity = targetVelocity;
        _lastVelocity = targetVelocity;
    }

    public void EndDash () {
        // Extra check to see if we are indeed dashing, because
        // if dash hasn't started, _gravityScaleBeforeDash
        // hasn't been set
        if (isDashing) {
            isDashing = false;
            _rigidbody.gravityScale = _gravityScaleBeforeDash;
        }
    }

    void UpdateProperties (Vector2 movement) {
        isFacingWall = Physics2D.OverlapCircle(_wallCheckPoint.position, _wallCheckRadius, _wallCheckMask) != null;

        bool wasGrounded = isGrounded;
        Collider2D groundCollider = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundCheckMask);
        isGrounded = groundCollider != null;

        if (isGrounded && !wasGrounded) {
            eventOnGrounded.Invoke(groundCollider);

            // Set moving platform rigidbody (if we are on a moving platform)
            movingPlatformRigidbody = groundCollider.GetComponentInParent<MovingPlatform>()?.rb;
        }

        if (!isGrounded && wasGrounded) {
            eventOnLeftGround.Invoke();

            // Can't be on moving platform if we are in the air
            movingPlatformRigidbody = null;
        }
        
        if (isFacingRight && movement.x < 0f && !isDashing) {
            isFacingRight = false;
        }

        if (!isFacingRight && movement.x > 0f && !isDashing) {
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
                currentJumps = 0;
            }

            if (jump && !isDashing) {
                if (canJumpFromGroundOrWall || currentJumps <= _extraAirJumps) {
                    // Jump
                    targetVelocity.y = jumpVelocity;
                    eventOnJump.Invoke();

                    // Add wall push for wall jumping
                    if (_enableWallJump && isHuggingWall) {
                        targetVelocity.x = isFacingRight ? -_wallJumpPushVelocity : _wallJumpPushVelocity;
                    }

                    _timeLeftToAllowJump = _jumpResetTime;
                    currentJumps++;
                }
            }
        } else {
            _timeLeftToAllowJump -= Time.fixedDeltaTime;
        }
    }

    void DashLogic (bool dash, ref Vector2 targetVelocity) {
        // Reset dash counter when able to jump
        if (canJumpFromGroundOrWall) {
            currentDashes = 0;
        }

        // Count down time left to allow dash
        if (_timeLeftToAllowDash > 0f) {
            _timeLeftToAllowDash -= Time.fixedDeltaTime;
        }

        // Check if we should initiate dashing
        if (_enableDashing && dash && !isDashing && currentDashes < _maxDashes && _timeLeftToAllowDash <= 0f) {
            _dashDistanceLeft = _dashDistance;
            isDashing = true;
            currentDashes++;
            _timeLeftToAllowDash = _dashResetTime;

            _gravityScaleBeforeDash = _rigidbody.gravityScale;
            _rigidbody.gravityScale = 0;
        }
        
        if (isDashing) {
            float distanceToMove = _dashSpeed * Time.fixedDeltaTime;

            // Make sure we don't overshoot
            if (_dashDistanceLeft - distanceToMove < 0f) {
                distanceToMove = _dashDistanceLeft;
            }

            // Will override rigidbody position (we want full control over dashing)
            _rigidbody.MovePosition(_rigidbody.position + distanceToMove * playerDirection);
            targetVelocity = _rigidbody.velocity;
            _dashDistanceLeft -= distanceToMove;

            // Check if dash is finished
            if (_dashDistanceLeft <= 0f) {
                EndDash();
                targetVelocity = Vector2.zero;
            }
        }
    }

    void FlipLogic () {
        if (_flipIfChangingDirection) {
            // Flip player when facing another direction
            bool flipX = _startFacingRight ? !isFacingRight : isFacingRight;
            transform.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);

            // Flip all child colliders because Unity doesn't like when colliders have
            // scale -1 :)
            foreach (Collider2D collider in _childColliders) {
                collider.transform.localScale = new Vector3(flipX ? 1f : -1f, 1f, 1f);
            }
        }
    }

    void Start () {
        isFacingRight = _startFacingRight;
        
        Collider2D[] attachedColliders = transform.GetComponents<Collider2D>();

        // Get all child colliders and don't include the ones on the current gameobject
        _childColliders = transform
            .GetComponentsInChildren<Collider2D>(true)
            .Where(c => !attachedColliders.Contains(c))
            .ToArray();

        // Register entity collision handler
        GetComponent<EntityCollision>().eventOnEntityCollisionEnter.AddListener(OnEntityCollisionEnter);
    }

    void OnEntityCollisionEnter (Entity entity) {
        // We want to end dash when colliding with another entity
        // as getting knocked back and then continuing to dash looks
        // strange :)
        EndDash();
    }

    void OnDrawGizmosSelected() {
        if (_groundCheckPoint != null) {
            Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
        }

        if (_wallCheckPoint != null) {
            Gizmos.DrawWireSphere(_wallCheckPoint.position, _wallCheckRadius);
        }
    }

}
