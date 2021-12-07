using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detects entity collision and works
/// properly even if entity has multiple colliders
/// </summary>
public class EntityCollision : MonoBehaviour {

    [SerializeField, Tooltip("Should trigger collision also be detected?")]
    bool _detectTriggerCollisions = true;

    // "Entity -> Amount of colliders colliding" dictionary
    Dictionary<Entity, int> _collidingEntities = new Dictionary<Entity, int>();

    UnityEvent<Entity> _eventOnEntityCollisionEnter = new UnityEvent<Entity>();
    UnityEvent<Entity> _eventOnEntityCollisionExit = new UnityEvent<Entity>();

    public UnityEvent<Entity> eventOnEntityCollisionEnter => _eventOnEntityCollisionEnter;
    public UnityEvent<Entity> eventOnEntityCollisionExit => _eventOnEntityCollisionExit;

    public bool IsCollidingWith (Entity entity) => _collidingEntities.ContainsKey(entity);

    void OnCollisionEnter2D(Collision2D collision) {
        AddColliderIfApplicable(collision.collider);
    }

    void OnCollisionExit2D(Collision2D collision) {
        RemoveColliderIfApplicable(collision.collider);
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (_detectTriggerCollisions) {
            AddColliderIfApplicable(collider);
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (_detectTriggerCollisions) {
            RemoveColliderIfApplicable(collider);
        }
    }

    void AddColliderIfApplicable(Collider2D collider) {
        Entity entity = collider.gameObject.GetComponentInParent<Entity>();

        if (entity != null) {
            // Create new dictionary entry if no colliders from this entity
            // has been detected before. Otherwise, count up number of colliders
            if (_collidingEntities.ContainsKey(entity)) {
                _collidingEntities[entity]++;
            } else {
                _collidingEntities[entity] = 1;
                _eventOnEntityCollisionEnter.Invoke(entity);
            }
        }
    }

    void RemoveColliderIfApplicable(Collider2D collider) {
        Entity entity = collider.gameObject.GetComponentInParent<Entity>();

        if (entity != null) {
            // Remove dictionary entry if no more colliders from this entity
            // are colliding. Otherwise, count down number of colliders

            _collidingEntities[entity]--;

            if (_collidingEntities[entity] == 0) {
                _collidingEntities.Remove(entity);
                _eventOnEntityCollisionExit.Invoke(entity);
            }
        }
    }

}