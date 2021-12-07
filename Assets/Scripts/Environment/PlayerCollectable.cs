using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectable : MonoBehaviour {

    int _orbsCollected;

    public int orbsCollected => _orbsCollected;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Collectable")) {
            _orbsCollected++;
            AudioManager.instance.PlaySound("orbcollect");
            Destroy(other.gameObject);
        }
    }

}
