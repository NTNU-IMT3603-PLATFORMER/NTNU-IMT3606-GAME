using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerEntity.INSTANCE.spirits += 1;
            Destroy(transform.parent.gameObject);
        }
    }

}
