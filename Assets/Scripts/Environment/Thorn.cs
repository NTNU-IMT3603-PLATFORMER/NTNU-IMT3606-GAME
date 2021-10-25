using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    // Amount of damage the thorn should do
    const int THORN_DAMAGE = 1;
    
    // Inflicts damage if the game object is an entity
    private void OnTriggerEnter2D(Collider2D trig) {
        Entity entity = trig.GetComponentInParent<Entity>();
        if (entity != null){
            entity.InflictDamage(THORN_DAMAGE);
        }
    }
}
