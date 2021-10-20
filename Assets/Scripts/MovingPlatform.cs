using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {   
    public Rigidbody2D rb;
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;

    float _distanceLeft;

    private void FixedUpdate() {
        if (_distanceLeft > 0f) {
            _distanceLeft -= moveSpeed * Time.fixedDeltaTime;
        }

        Vector2 direction = (waypoints[target].position.ToVec2() - rb.position).normalized;
        rb.velocity = moveSpeed * direction;

        //use data
        if(_distanceLeft <= 0f)
        {
            if(target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }

            _distanceLeft = (waypoints[target].position.ToVec2() - rb.position).magnitude;
        }
    }

}
