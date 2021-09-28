using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    public Rigidbody2D rb;

    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.21f;
    public UnityEngine.LayerMask groundCheckMask;

    public Transform wallCheckPoint;
    public float wallCheckRadius = 0.35f;
    public UnityEngine.LayerMask wallCheckMask;

    public float jumpHeight = 5f;
    public float jumpResetTime = 0.25f;
    public int extraAirJumps = 1;
    public bool flipIfChangingDirection = true;
    public bool startFacingRight = true;

    public bool enableWallSlide = true;
    public float minWallSlideGravityVelocity = -2f;

    public bool enableWallJump = true;

    public float jumpVelocity {
        get {
            return Mathf.Sqrt(2 * -Physics2D.gravity.y * rb.gravityScale * jumpHeight);
        }
    }

    public bool isGrounded { get; private set; }
    public bool isFacingWall { get; private set; }
    public bool isFacingRight { get; private set; }
    public bool isHuggingWall { get; private set; }
    public bool canJumpFromGroundOrWall { get; private set; }

    private float timeLeftToAllowJump;
    private int currentJumps;

    void Start () {
        isFacingRight = startFacingRight;
    }

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
