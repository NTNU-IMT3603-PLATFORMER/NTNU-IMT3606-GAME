using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlatformDetector : MonoBehaviour
{
    public Transform player;
    public bool isGrounded;
    public bool check;

    void Update()
    {
        isGrounded = PlatformerCharacter2D.m_Grounded;

        if(isGrounded != true)
        {
            check = false;
        }

        if(check != false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .125f);

            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    player.SetParent(hit.transform);
                }
                else
                {
                    player.SetParent(null);
                }
                check = true;
            }
        }
    }
}
