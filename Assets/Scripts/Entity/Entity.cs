using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour {

    [SerializeField, Tooltip("The rigidbody of the entity")]
    Rigidbody2D _rigidbody;

    [SerializeField, Tooltip("The health of the entity")]                       int _health;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")] bool _invincible;

    /// <summary>
    /// The health of the entity
    /// </summary>
    public int health => _health;
    
    /// <summary>
    /// If true, inflicting damage will have no effect
    /// </summary>
    public bool invincible {
        get => _invincible;
        set => _invincible = value;
    }

    public abstract void Respawn();
    public abstract void Die();

    public virtual void InflictDamage(int damage, Transform opponentTransform) {
        if (invincible) {
            return;
        }

        _health -= damage;

        if (_health <= 0) {
            Die();
        }

        Knockback(opponentTransform);
    }

    public void Knockback(Transform opponentTransform) {
        Vector2 moveDirection = _rigidbody.transform.position - opponentTransform.transform.position;

        if(moveDirection == Vector2.zero) {
            moveDirection = Vector2.one;
        }
        Debug.Log(moveDirection.normalized);
        _rigidbody.velocity = moveDirection.normalized * 10f;
    }

    public void onhitFlash() {
        
    }
    
}
