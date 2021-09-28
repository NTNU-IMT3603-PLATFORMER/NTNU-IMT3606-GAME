using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool moveRight;
    [SerializeField]
    private bool patrolMode;
    [SerializeField]
    private float followDistance;
    private SpriteRenderer sr;
    private Transform target;
    private bool followPlayer;

    private string BOUNDARY_TAG = "Boundary";

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, target.position) < followDistance) && !followPlayer && !patrolMode)
        {
            followPlayer = true;
        }

        if (followPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (transform.position.x > target.position.x)
            {
               sr.flipX = false;
            } else
            {
               sr.flipX = true;
            }
        }

        else if (moveRight)
        {
            transform.Translate(speed * Time.deltaTime * speed, 0, 0);
            sr.flipX = true;
        }
        else 
        { 
            transform.Translate(-speed * Time.deltaTime * speed, 0, 0);
            sr.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag(BOUNDARY_TAG))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
    }
}
