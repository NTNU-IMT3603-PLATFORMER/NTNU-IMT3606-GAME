using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : Entity {


    [SerializeField, Tooltip("The range of the player attack as a float")]
    float _attackRange;
    [SerializeField, Tooltip("How often the player can attack")]
    float _attackRate;

    [SerializeField, Tooltip("The attack point of the player")]
    Transform _attackPoint;
    [SerializeField, Tooltip("The layer which should be counted as enemies")]
    LayerMask _enemyLayers;

    /// <summary>
    /// The range of the player attack
    /// </summary>
    public float attackRange {
        get => _attackRange;
        set => _attackRange = value;
    }

    /// <summary>
    /// How often the player can attack
    /// </summary>
    public float attackRate {
        get => _attackRate;
        set => _attackRate = value;
    }


    float _nextAttackTime = 0f;

    UnityEvent _eventOnAttack = new UnityEvent();

    /// <summary>
    /// Unity event for when the player is attacking
    /// </summary>
    public UnityEvent eventOnAttack => _eventOnAttack;

    // Update is called once per frame
    void Update()
    {
        if (_nextAttackTime <= 0) {
            if (Input.GetButtonDown("Attack")) {
                Attack();
                _nextAttackTime = _attackRate;
            }
        } else {
            _nextAttackTime -= Time.deltaTime; 
        }
    }

    void Attack() {
        // Invokes the listener of eventOnAttack 
        eventOnAttack.Invoke();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(baseDamage);
        }
    }


    public override void Respawn() {
    }

    public override void Die() {
        Debug.Log("Player died");
    }

    private void OnDrawGizmosSelected() {
        if (_attackPoint == null)
            return;

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
