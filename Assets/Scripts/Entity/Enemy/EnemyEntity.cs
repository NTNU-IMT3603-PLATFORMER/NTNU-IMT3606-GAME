using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEntity : Entity {

    [SerializeField, Tooltip("The layer which contains the player character")]
    LayerMask _playerLayers;
    [SerializeField, Tooltip("How often the enemy can do contact damage")]
    float _contactDamageRate;

    public Transform respawnPoint;
    [SerializeField, Tooltip("What prefab to respawn")]
    GameObject LastEnemy;
    GameObject enemy;

    UnityEvent _eventOnDeath = new UnityEvent();
    public UnityEvent eventOnDeath => _eventOnDeath;

    public override void Respawn() {
        GameObject prefab = Resources.Load<GameObject>("CoolEnemy");

        GameObject clone = (GameObject)Instantiate(prefab, respawnPoint.position, Quaternion.identity);
        clone.name = prefab.name;
    }

    public override void Die() {
        Debug.Log("Enemy died!");
        
        eventOnDeath.Invoke();
        Respawn();
        Destroy(gameObject);

    }

}
