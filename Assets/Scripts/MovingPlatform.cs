using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{   
    public Rigidbody2D rb;
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;

    /* void Update()
    {
        //receive data
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
    } */

    private void FixedUpdate() 
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime));
        //use data
        if(transform.position == waypoints[target].position)
        {
            if(target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target += 1;
            }
        }
    }
}
