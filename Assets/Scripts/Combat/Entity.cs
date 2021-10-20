using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour {

<<<<<<< HEAD
    [SerializeField, Tooltip("The rigidbody of the entity")]
    Rigidbody2D _rigidbody;

    [SerializeField, Tooltip("The health of the entity")]
    int _health;
    [SerializeField, Tooltip("The damage the entity does to other entities")]
    int _baseDamage;
=======
    [SerializeField, Tooltip("The health of the entity")]                       int _health;
    [SerializeField, Tooltip("The damage the entity does to other entities")]   int _baseDamage;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")] bool _invincible;
>>>>>>> c0d4f71 (Scripts: PB Boss is now invincible while charging)


    /// <summary>
    /// The health of the entity
    /// </summary>
    public int health => _health;

    /// <summary>
    /// The damage the entity does to other entities
    /// </summary>
<<<<<<< HEAD
    public int baseDamage {
        get => _baseDamage;
        set => _baseDamage = value;
    }

    public int health => _health;
=======
    public int baseDamage => _baseDamage;
    
    /// <summary>
    /// If true, inflicting damage will have no effect
    /// </summary>
    public bool invincible {
        get => _invincible;
        set => _invincible = value;
    }
>>>>>>> c0d4f71 (Scripts: PB Boss is now invincible while charging)

    public abstract void Respawn();
    public abstract void Die();

    public virtual void InflictDamage(int damage) {
        if (invincible) {
            return;
        }

        _health -= damage;

        if (_health <= 0) {
            Die();
        }
        Knockback();
    }

    public void Knockback() {
        Collider2D[] otherEntity = Physics2D.OverlapBoxAll(_rigidbody.transform.position, new Vector2(1,1), 0);
        Vector2 moveDirection = new Vector2(0,0);
        foreach(Collider2D entity in otherEntity) {
            if(entity.tag != _rigidbody.tag) {
                moveDirection = _rigidbody.transform.position - entity.transform.position;
                Debug.Log(entity.tag);
            }
        }
        
        if(moveDirection == new Vector2(0,0)) {
            moveDirection = new Vector2(1, 1);
        }
        Debug.Log(moveDirection.normalized);
        _rigidbody.velocity = moveDirection.normalized * 10f;
    }
    
}
