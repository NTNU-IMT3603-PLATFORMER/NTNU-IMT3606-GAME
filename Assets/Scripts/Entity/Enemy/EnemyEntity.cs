using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Entity for enemy types
/// </summary>
public class EnemyEntity : Entity {

    [SerializeField, Tooltip("The layer which contains the player character")]
    LayerMask _playerLayers;
    [SerializeField, Tooltip("How often the enemy can do contact damage")]
    float _contactDamageRate;
    [SerializeField, Tooltip("Point where entity will respawn if applicable")]
    Transform _respawnPoint;
    [SerializeField, Tooltip("What spirit drop prefab to drop")]
    GameObject _spiritDropPrefab;

    GameObject _spiritTarget;
    UnityEvent _eventOnDeath = new UnityEvent();

    /// <summary>
    /// Invoked when enemy entity dies
    /// </summary>
    public UnityEvent eventOnDeath => _eventOnDeath;

    public override void Respawn() {
        // TODO: Should probably cache this at some point in the future
        GameObject prefab = Resources.Load<GameObject>("CoolEnemy");

        GameObject clone = Instantiate<GameObject>(prefab, _respawnPoint.position, Quaternion.identity);
        clone.name = prefab.name;
    }

    public override void Die() {
        AudioManager.instance.PlaySound("enemydeath");
        eventOnDeath.Invoke();
        StartCoroutine(OnDie());
    }

    public override IEnumerator OnHitEffects() {
        AudioManager.instance.PlaySound("enemydamage");
        yield return base.OnHitEffects();
    }

    void Start() {
        // Auto-generate respawn point if a manual one is not set
        if (_respawnPoint == null) {
            _respawnPoint = new GameObject("(AUTO) EnemyRespawnPoint").transform;
            _respawnPoint.position = transform.position;
        }
    }

    IEnumerator OnDie() {
        yield return new WaitForSeconds(lastBreathTime);
        Destroy(gameObject);

        _spiritTarget = GameObject.FindGameObjectWithTag("Player");

        Instantiate<GameObject>(_spiritDropPrefab, transform.position, Quaternion.identity);

        if (shouldRespawn) {
            RespawnAfterRespawnTime();
        }
    }

}
