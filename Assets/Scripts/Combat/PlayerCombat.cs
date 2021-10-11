using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Entity {

    [SerializeField, Tooltip("The range of the player attack as a float")]
    float _attackRange;
    [SerializeField, Tooltip("How often the player can attack")]
    float _attackRate;

    float _nextAttackTime = 0f;

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextAttackTime) {
            if (Input.GetKeyDown(KeyCode.X)) {
                Attack();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    void Attack() {
        animator.SetTrigger("isAttacking");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(_baseDamage);
        }
    }


    public override void Respawn() {
    }

    public override void Die() {
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
    }
}
