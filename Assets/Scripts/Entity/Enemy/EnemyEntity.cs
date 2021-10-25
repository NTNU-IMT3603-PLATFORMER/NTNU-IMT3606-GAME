using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEntity : Entity {

    [SerializeField, Tooltip("The layer which contains the player character")]  LayerMask _playerLayers;
    [SerializeField, Tooltip("How often the enemy can do contact damage")]      float _contactDamageRate;
    [SerializeField, Tooltip("Point where entity will respawn if applicable")]  Transform respawnPoint;

    UnityEvent _eventOnDeath = new UnityEvent();
    public UnityEvent eventOnDeath => _eventOnDeath;

    public override void Respawn() {
        // TODO: Should probably cache this at some point in the future
        GameObject prefab = Resources.Load<GameObject>("CoolEnemy");

        GameObject clone = Instantiate<GameObject>(prefab, respawnPoint.position, Quaternion.identity);
        clone.name = prefab.name;
    }

    public override void Die() {
        Debug.Log("Enemy died!");
        
        eventOnDeath.Invoke();
        Destroy(gameObject);

        if (shouldRespawn) {
            RespawnAfterRespawnTime();
        }
    }

    void Start () {
        // Auto-generate respawn point if a manual one is not set
        if (respawnPoint == null) {
            respawnPoint = new GameObject("(AUTO) EnemyRespawnPoint").transform;
            respawnPoint.position = transform.position;
        }
    }

}
