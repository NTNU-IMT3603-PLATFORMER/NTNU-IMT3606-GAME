using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.41f;
    public UnityEngine.LayerMask groundCheckMask;

    public Transform wallCheckPoint;
    public float wallCheckRadius = 0.41f;
    public UnityEngine.LayerMask wallCheckMask;

    public float jumpHeight = 5f;
    public float jumpResetTime = 0.5f;
    public bool flipIfChangingDirection = true;
    public bool startFacingRight = true;

    public bool isGrounded { get; private set; }
    public bool isFacingWall { get; private set; }
    public bool isFacingRight { get; private set; }

    private float jumpTime;

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

        // Jump-related logic
        if (jumpTime <= 0f) {
            if (jump && isGrounded) {
                // Jump (calculate target velocity based on jump height and gravity)
                targetVelocity.y = Mathf.Sqrt(2 * -Physics2D.gravity.y * rb.gravityScale * jumpHeight);

                jumpTime = jumpResetTime;
            }
        } else {
            jumpTime -= Time.fixedDeltaTime;
        }

        // Apply target velocity
        rb.velocity = targetVelocity;

        // Flip sprite renderer when facing another direction
        spriteRenderer.flipX = startFacingRight ? !isFacingRight : isFacingRight;
    }

}
