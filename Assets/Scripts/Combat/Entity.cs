using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour {
    [SerializeField, Tooltip("The health of the entity")]
    int _health;
    [SerializeField, Tooltip("The damage the entity does to other entities")]
    int _baseDamage;

    /// <summary>
    /// The damage the entity does to other entities
    /// </summary>
    public int baseDamage => _baseDamage;

    public abstract void Respawn();
    public abstract void Die();

    public void TakeDamage(int damage) {
        _health -= damage;

        if (_health <= 0) {
            Die();
        }
    }

}
