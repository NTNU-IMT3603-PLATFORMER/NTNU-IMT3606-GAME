using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float travelLength;

    [SerializeField]
    private bool MoveRight;

    private Animator animator;

    private SpriteRenderer sr;

    //private string IS_MOVING_TAG = "isMoving";

    //private string BOUNDARY_TAG = "Boundary";

    // Start is called before the first frame update
    void Start()
    {
          sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
        }
        else
        { 
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);   
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Boundary"))
        {
            if (MoveRight)
            {
                MoveRight = false;
                sr.flipX = false;
            }
            else
            {
                MoveRight = true;
                sr.flipX = true;
            }
        }
    }
}
