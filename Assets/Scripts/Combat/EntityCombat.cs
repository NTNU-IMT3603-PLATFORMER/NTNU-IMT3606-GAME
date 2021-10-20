using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityCombat : MonoBehaviour {

    [SerializeField, Tooltip("The range of the player attack as a float")]
    float _attackRange;

    [SerializeField, Tooltip("The attack point of the player")]
    Transform attackPoint;
    [SerializeField, Tooltip("The layer which should be counted as enemies")]
    LayerMask enemyLayers;

    UnityEvent _eventOnAttack = new UnityEvent();

    /// <summary>
    /// Unity event for when the player is attacking
    /// </summary>
    public UnityEvent eventOnAttack => _eventOnAttack;

    /// <summary>
    /// Perform an attack.
    /// Will attack all enemies within range
    /// </summary>
    public virtual void Attack(int damage) {
        // Invokes the listener of eventOnAttack 
        eventOnAttack.Invoke();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackRange, enemyLayers);

        // We want unique entities because some entities might have multiple colliders
        IEnumerable<Entity> uniqueEntities = hitEnemies.Select(c => c.GetComponentInParent<Entity>()).Distinct();

        foreach(Entity entity in uniqueEntities) {
            Debug.Log("We hit " + entity.name);
            entity.InflictDamage(damage);
        }
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
    }


}
