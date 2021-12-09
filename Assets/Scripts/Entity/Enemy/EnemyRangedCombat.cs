using UnityEngine;

/// <summary>
/// Responsible for controlling enemy combat for ranged enemies
/// </summary>
public class EnemyRangedCombat : EnemyCombat {

    [Header("Ranged attack properties")]
    [SerializeField]
    GameObject _prefabRangedAttack;
    [SerializeField]
    float _projectileSpeed = 5;
    [SerializeField]
    float _projectileTime = 2f;

    /// <summary>
    /// Ranged attack
    /// </summary>
    /// <param name="damage">The damage the attack does</param>
    public override void Attack(int damage) {
        // Instantiate a ranged attack at the attack position
        GameObject rangedAttackObject = Instantiate<GameObject>(_prefabRangedAttack, attackPoint.position, Quaternion.identity);
        RangedAttack rangedAttack = rangedAttackObject.GetComponent<RangedAttack>();
        rangedAttack.projectileSpeed = _projectileSpeed;
        rangedAttack.timeLeft = _projectileTime;
        timeLeftToAllowAttack = attackCooldown;
    }

}
