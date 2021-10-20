using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity {

    [SerializeField, Tooltip("The layer which contains the player character")]
    LayerMask _playerLayers;
    [SerializeField, Tooltip("How often the enemy can do contact damage")]
    float _contactDamageRate;

    float _nextAttackTime = 0f;

    public Transform respawnPoint;
    [SerializeField, Tooltip("What prefab to respawn")]
    GameObject _entityPrefab;
    GameObject LastEnemy;
    GameObject enemy;


    void Update() {
        if (_nextAttackTime <= 0) {
            Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, _playerLayers);
            foreach (Collider2D player in hitPlayer) {
                player.GetComponent<PlayerEntity>().InflictDamage(baseDamage);
                _nextAttackTime = _contactDamageRate;
            }
        } else {
            _nextAttackTime -= Time.deltaTime; 
        }

    }

    public override void Respawn() {
        GameObject clone = (GameObject)Instantiate(Resources.Load<GameObject>("CoolEnemy"), respawnPoint.position, Quaternion.identity);
        LastEnemy = GameObject.Find(_entityPrefab.name + "(Clone)");
        LastEnemy.name = _entityPrefab.name;
        
    }

    public override void Die() {
        Debug.Log("Enemy died!");

        //gameObject.SetActive(false);
        
        
        Respawn();
        Destroy(gameObject);

    }


    public void Attack() {

    }

}
