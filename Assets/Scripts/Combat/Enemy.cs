using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    LayerMask _playerLayers;
    [SerializeField, Tooltip("How often the enemy can do contact damage")]
    float _contactDamageRate;

    float _nextAttackTime = 0f;


    void Update() {
        if (_nextAttackTime <= 0) {
            Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0, _playerLayers);
            foreach (Collider2D player in hitPlayer) {
                player.GetComponent<PlayerCombat>().TakeDamage(baseDamage);
                _nextAttackTime = _contactDamageRate;
            }
        } else {
            _nextAttackTime -= Time.deltaTime; 
        }

    }

    public override void Respawn() {
    }

    public override void Die() {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }


    public void Attack() {

    }
}
