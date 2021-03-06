using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for all entities in the game
/// </summary>
public abstract class Entity : MonoBehaviour {

    protected const float ON_HIT_FLASH_TIME = 0.3f;

    protected CharacterController2D _characterController2D;
    protected Renderer _renderer;

    [SerializeField, Tooltip("The rigidbody of the entity")]
    Rigidbody2D _rigidbody;
    [SerializeField, Tooltip("The health of the entity")]
    int _health = 10;
    [SerializeField, Tooltip("The max health of the entity")]
    int _maxHealth = 10;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")]
    bool _invincible;
    [SerializeField, Tooltip("The color the entity will get when hit")]
    Color _onhitColor;
    [SerializeField, Tooltip("Time to respawn after death")]
    float _respawnTime = 10f;
    [SerializeField, Tooltip("Should the entity respawn after death?")]
    bool _shouldRespawn = true;
    [SerializeField, Tooltip("Time to wait until destroying object after death")]
    float _lastBreathTime = 1f;

    UnityEvent<int> _eventOnTakingDamage = new UnityEvent<int>();

    /// <summary>
    /// The character controller used by this entity
    /// </summary>
    public CharacterController2D characterController2D => _characterController2D;

    /// <summary>
    /// Events that is invoked when something has inflicted damage to this entity
    /// </summary>
    public UnityEvent<int> eventOnTakingDamage => _eventOnTakingDamage;

    /// <summary>
    /// The max health of the entity
    /// </summary>
    public int maxHealth {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    /// <summary>
    /// The health of the entity
    /// </summary>
    public int health {
        get => _health;
        set => _health = value;
    }

    /// <summary>
    /// If true, inflicting damage will have no effect
    /// </summary>
    public bool invincible {
        get => _invincible;
        set => _invincible = value;
    }

    /// <summary>
    /// Should the entity respawn after death?
    /// </summary>
    public bool shouldRespawn {
        get => _shouldRespawn;
        set => _shouldRespawn = value;
    }

    /// <summary>
    /// Time to wait until destroying object after death
    /// </summary>
    public float lastBreathTime => _lastBreathTime;

    /// <summary>
    /// Revive this entity or spawn a new one
    /// </summary>
    public abstract void Respawn();

    /// <summary>
    /// Will get called when this entity dies
    /// </summary>
    public abstract void Die();

    /// <summary>
    /// Add 1 to entity's blood bar if applicable
    /// </summary>
    public virtual void AddBlood() { }

    /// <summary>
    /// Call respawn after provided amount of seconds.
    /// Works even if you destroy this entity
    /// </summary>
    public void RespawnAfterSeconds(float seconds) {
        RunAfterSeconds.Create(seconds, () => Respawn());
    }

    /// <summary>
    /// Respawn entity after waiting respawn time.
    /// Works even if you destroy this entity
    /// </summary>
    public void RespawnAfterRespawnTime() {
        RespawnAfterSeconds(_respawnTime);
    }

    /// <summary>
    /// Called every update. 
    /// Use this instead of regular Update
    /// </summary>  
    public virtual void UpdateEntity() {
        // Die if falling outside world
        if (transform.position.y < -100) {
            Die();
        }
    }

    /// <summary>
    /// Decreases health of this entity.
    /// Might also cause side effects such as knockback
    /// and other visual effects.
    /// Does nothing if entity is invincible.
    /// </summary>
    public virtual void InflictDamage(int damage, Vector3 hitPosition, float knockbackAmount) {
        if (invincible) {
            return;
        }

        _characterController2D.isHit = true;
        _characterController2D.EndDash();

        _health -= damage;

        if (_health <= 0) {
            Die();
        }

        // Start hit effects
        StartCoroutine(OnHitEffects());

        // Start coroutine ensuring player cannot move for a short period
        StartCoroutine(OnHitNoMove());

        if (knockbackAmount != 0f) {
            Knockback(hitPosition, knockbackAmount);
        }

        eventOnTakingDamage.Invoke(damage);
    }

    /// <summary>
    /// Causes this entity to get knocked back
    /// by setting rigidbody velocity
    /// </summary>
    public void Knockback(Vector3 hitPosition, float knockbackAmount) {
        Vector2 moveDirection = _rigidbody.transform.position - hitPosition;

        if (moveDirection == Vector2.zero) {
            moveDirection = Vector2.one;
        }

        _rigidbody.velocity = moveDirection.normalized * knockbackAmount;
    }

    /// <summary>
    /// (Visual) effects that should be applied when 
    /// this entity is taking damage (hit). 
    /// </summary>
    public virtual IEnumerator OnHitEffects() {
        _renderer = GetComponentInChildren<Renderer>();

        Color defaultColor = _renderer.material.color;
        Color onHitColor = _onhitColor;

        onHitColor.a = defaultColor.a;
        _renderer.material.color = onHitColor;
        yield return new WaitForSeconds(ON_HIT_FLASH_TIME);
        _renderer.material.color = defaultColor;
    }

    IEnumerator OnHitNoMove() {
        yield return new WaitForSeconds(0.3f);
        _characterController2D.isHit = false;
    }

    void Awake() {
        _characterController2D = GetComponent<CharacterController2D>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    void Update() {
        UpdateEntity();
    }

}
