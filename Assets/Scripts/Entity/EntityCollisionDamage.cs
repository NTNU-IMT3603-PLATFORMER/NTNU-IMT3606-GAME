using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for dealing damage when colliding with other entities
/// </summary>
[RequireComponent(typeof(EntityCollision))]
public class EntityCollisionDamage : MonoBehaviour {

    [SerializeField, Tooltip("How much damage should the player take?")]                    int _damage = 1;

    EntityCollision _entityCollision;
    float _damageTimer;

    void Start () {
        _entityCollision = GetComponent<EntityCollision>();
        
        _entityCollision.eventOnEntityCollisionEnter.AddListener(OnEntityCollisionEnter);
    }

    void OnEntityCollisionEnter (Entity entity) {
        entity.InflictDamage(_damage, transform, 10f);
    }

}