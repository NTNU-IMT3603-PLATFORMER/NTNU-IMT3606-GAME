using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    public Rigidbody2D rb;
    public Transform groundCheckPoint;
    public float groundRadius = 0.41f;
    public UnityEngine.LayerMask groundMask;
    public float jumpHeight = 5f;
    public float jumpResetTime = 0.5f;
    public bool flipIfChangingDirection = true;

    public bool grounded { get; private set; }

    private float jumpTime;

    public void Move (Vector2 movement, bool jump) {
        Vector2 targetVelocity = rb.velocity;
        targetVelocity.x = movement.x;
        targetVelocity.y += movement.y;

        if (jumpTime <= 0f) {
            if (jump && grounded) {
                // Jump
                targetVelocity.y = Mathf.Sqrt(2 * -Physics2D.gravity.y * rb.gravityScale * jumpHeight);

                jumpTime = jumpResetTime;
            }
        } else {
            jumpTime -= Time.fixedDeltaTime;
        }

        if (flipIfChangingDirection) {
            transform.localScale = new Vector3(movement.x < 0f ? -1f : 1f, transform.localScale.y, transform.localScale.z);
        }

        // Apply velocity
        rb.velocity = targetVelocity;

        // Check if grounded
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundRadius, groundMask) != null;
    }

}
