using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour {

    // Amount of damage the thorn should do
    const int THORN_DAMAGE = 1;

    Collider2D _collider;

    void Start () {
        _collider = GetComponent<Collider2D>();
    }
    
    // Inflicts damage if the game object is an entity
    private void OnTriggerStay2D(Collider2D collider) {
        Entity entity = collider.GetComponentInParent<Entity>();

        if (entity != null){
            entity.InflictDamage(THORN_DAMAGE, _collider.ClosestPoint(entity.transform.position), 10f);
        }
    }

}
