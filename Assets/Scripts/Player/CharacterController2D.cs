using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    public Rigidbody2D rb;
    public Transform groundRayOrigin;
    public float groundRayLength = 0.1f;
    public UnityEngine.LayerMask groundMask;
    public bool applyGravity = true;
    public float gravityFactor = 1f;
    public float jumpFactor = 0.5f;
    public bool flip = true;

    public bool grounded { get; private set; }
    public RaycastHit2D lastGroundHit { get; private set; }

    // Forces when mass is 1 (constant)
    private Dictionary<string, Vector2> forces;

    private float jumpTime;
    private Vector2 currentForceAppliedVelocity;

    void Awake () {
        forces = new Dictionary<string, Vector2>() {
            {"g", Physics2D.gravity * gravityFactor},
            {"n", -Physics2D.gravity * gravityFactor}
         };
    }

    public void Move (Vector2 movement, bool jump) {
        // Set normal force when grounded
        if (grounded) {
            forces["n"] = -forces["g"];
        } else {
            forces["n"] = Vector3.zero;
        }

        // Add up normalized forces for resulting acceleration
        Vector2 resultingAcceleration = Vector2.zero;

        foreach (Vector2 force in forces.Values) {
            resultingAcceleration += force;
        }

        if (jumpTime <= 0f) {
            if (jump && grounded) {
                jumpTime = 0.5f;
                currentForceAppliedVelocity = -forces["g"] * jumpFactor;
            }

            if (!jump && grounded) {
                // Reset velocity when grounded
                currentForceAppliedVelocity = Vector2.zero;
            }
        } else {
            jumpTime -= Time.deltaTime;
        }

        // Adjust velocity based on calculated resulting acceleration
        currentForceAppliedVelocity += resultingAcceleration * Time.deltaTime;
        movement += currentForceAppliedVelocity;

        if (flip) {
            transform.localScale = new Vector3(movement.x < 0f ? -1f : 1f, transform.localScale.y, transform.localScale.z);
        }

        // Apply movement
        transform.position += movement.ToVec3() * Time.deltaTime;

        CheckGround();

        Vector3 contactPoint = groundRayOrigin.position + Vector3.down * groundRayLength;

        if (lastGroundHit.transform != null && contactPoint.y < lastGroundHit.point.y) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (lastGroundHit.point.y - contactPoint.y), transform.position.z);
        }
    }

    public void CheckGround () {
        RaycastHit2D hit = Physics2D.Raycast(groundRayOrigin.position.ToVec2(), Vector2.down, groundRayLength, groundMask);

        grounded = hit.transform != null;
        lastGroundHit = hit;
    }

	
    void OnDrawGizmos () {
        Debug.DrawLine(groundRayOrigin.position, groundRayOrigin.position + Vector3.down * groundRayLength, Color.red);
    }

}
