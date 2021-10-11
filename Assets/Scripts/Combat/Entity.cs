using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour {
    [Tooltip("The health of the entity")]
    public int _health;
    [Tooltip("The damage the entity does to other entities")]
    public int _baseDamage;

    public abstract void Respawn();
    public abstract void Die();

    public void TakeDamage(int damage) {
        _health -= damage;

        if (_health <= 0) {
            Die();
        }
    }

}
