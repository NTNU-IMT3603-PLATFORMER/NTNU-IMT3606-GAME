using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().spirits += 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
