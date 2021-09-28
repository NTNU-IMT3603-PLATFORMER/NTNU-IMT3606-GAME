using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [Tooltip("Mandatory rigidbody that will be used for moving character")] public Rigidbody2D rb;

    [Header("Ground / Wall Detection")]

    [Tooltip("Transform representing point that will be used as origin for ground check")] public Transform groundCheckPoint;
    [Tooltip("Radius of circle for ground detection")] public float groundCheckRadius = 0.21f;
    [Tooltip("Layer that should be detected as ground")] public UnityEngine.LayerMask groundCheckMask;

    [Tooltip("Transform representing point that will be used as origin for wall check")] public Transform wallCheckPoint;
    [Tooltip("Radius of circle for wall detection")] public float wallCheckRadius = 0.35f;
    [Tooltip("Layer that should be detected as wall")] public UnityEngine.LayerMask wallCheckMask;

    [Header("Jumping")]

    [Tooltip("The height that each jump should reach")] public float jumpHeight = 5f;
    [Tooltip("Time after each jump where jumping again should not be allowed")] public float jumpResetTime = 0.25f;
    [Tooltip("Additional jumps that are allowed while in air")] public int extraAirJumps = 1;

    [Header("Player direction")]

    [Tooltip("Should transform z scale be flipped when changing movement direction?")] public bool flipIfChangingDirection = true;
    [Tooltip("Does player sprite start facing right?")] public bool startFacingRight = true;

    [Header("Wall sliding / jumping")]

    [Tooltip("Should wall sliding be enabled?")] public bool enableWallSlide = true;
    [Tooltip("Minimum y velocity allowed when wall sliding. Used to prevent full force of gravity.")] public float minWallSlideGravityVelocity = -2f;
    [Tooltip("Should wall jumping be enabled?")] public bool enableWallJump = true;

    /// <summary>
    /// Velocity for player jumping. Calculated using jumpHeight
    /// </summary>
    public float jumpVelocity => Mathf.Sqrt(2 * -Physics2D.gravity.y * rb.gravityScale * jumpHeight);

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
    /// Only checks if grounded if wall jumping is disabled
    /// </summary>
    public bool canJumpFromGroundOrWall { get; private set; }

    private float timeLeftToAllowJump;
    private int currentJumps;

    void Start () {
        isFacingRight = startFacingRight;
    }

    /// <summary>
    /// Move the character. 
    /// Should be called from FixedUpdate
    /// </summary>
    public void Move (Vector2 movement, bool jump) {
        // Create a target velocity that will be applied later in the script
        // Include gravity for y component
        Vector2 targetVelocity = new Vector2(movement.x, rb.velocity.y + movement.y);

        // Update properties relating to state of player
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundCheckMask) != null;
        isFacingWall = Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRadius, wallCheckMask) != null;
        
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

        canJumpFromGroundOrWall = isGrounded || (enableWallJump && isHuggingWall);

        // Wall-slide logic
        if (enableWallSlide) {
            if (isHuggingWall) {
                targetVelocity.y = Mathf.Clamp(targetVelocity.y, minWallSlideGravityVelocity, float.MaxValue);
            }
        }

        // Jump-related logic
        if (timeLeftToAllowJump <= 0f) {
            // Reset current jumps when hitting ground / wall
            if (canJumpFromGroundOrWall) {
                currentJumps = 0;
            }

            if (jump) {
                if (canJumpFromGroundOrWall || currentJumps <= extraAirJumps) {
                    // Jump
                    targetVelocity.y = jumpVelocity;
                    timeLeftToAllowJump = jumpResetTime;
                    currentJumps++;
                }
            }
        } else {
            timeLeftToAllowJump -= Time.fixedDeltaTime;
        }

        // Apply target velocity
        rb.velocity = targetVelocity;

        // Flip player when facing another direction
        bool flipX = startFacingRight ? !isFacingRight : isFacingRight;
        transform.localScale = new Vector3(flipX ? -1f : 1f, 1f, 1f);
    }

}
