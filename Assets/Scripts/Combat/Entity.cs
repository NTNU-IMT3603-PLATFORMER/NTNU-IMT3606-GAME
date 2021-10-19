using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour {

    //[SerializeField, Tooltip("The Character Controller")]
    //CharacterController2D _characterController2D;
    [SerializeField, Tooltip("The rigidbody of the entity")]
    Rigidbody2D _rigidbody;
    [SerializeField, Tooltip("The health of the entity")]
    int _health;
    [SerializeField, Tooltip("The damage the entity does to other entities")]
    int _baseDamage;


    /// <summary>
    /// The damage the entity does to other entities
    /// </summary>
    public int baseDamage {
        get => _baseDamage;
        set => _baseDamage = value;
    }

    public int health => _health;

    public abstract void Respawn();
    public abstract void Die();

    public void TakeDamage(int damage) {
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
        _rigidbody.AddForce(moveDirection.normalized * 200f);
    }
}
