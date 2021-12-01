using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectable : MonoBehaviour
{
    [SerializeField] public int orbs;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Collectable"))
        {
            orbs++;
            Destroy(other.gameObject);
        }
    }
}
