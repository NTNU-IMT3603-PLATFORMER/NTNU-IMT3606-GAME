using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour {

    protected const float ON_HIT_FLASH_TIME = 0.3f;

    [SerializeField, Tooltip("The rigidbody of the entity")]                        Rigidbody2D _rigidbody;
    [SerializeField, Tooltip("The health of the entity")]                           int _health = 10;
    [SerializeField, Tooltip("The max health of the entity")]                       int _maxHealth = 10;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")]     bool _invincible;
    [SerializeField, Tooltip("The color the entity will get when hit")]             Color _onhitColor;
    [SerializeField, Tooltip("Time to respawn after death")]                        float _respawnTime = 10f;
    [SerializeField, Tooltip("Should the entity respawn after death?")]             bool _shouldRespawn = true;

    protected CharacterController2D _characterController2D;
    protected Renderer _renderer;

    UnityEvent<int> _eventOnTakingDamage = new UnityEvent<int>();

    void Awake() {
        _characterController2D = GetComponent<CharacterController2D>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    public CharacterController2D characterController2D =>_characterController2D;

    /// <summary>
    /// Events that is invoked when something has inflicted damage to this entity
    /// </summary>
    public UnityEvent<int> eventOnTakingDamage => _eventOnTakingDamage;

    /// <summary>
    /// The max health of the entity
    /// </summary>
    public int maxHealth => _maxHealth;

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

    public abstract void Respawn();
    public abstract void Die();

    /// <summary>
    /// Add 1 to entity's blood bar if applicable
    /// </summary>
    public virtual void AddBlood() {}

    /// <summary>
    /// Call respawn after provided amount of seconds.
    /// Works even if you destroy this entity
    /// </summary>
    public void RespawnAfterSeconds (float seconds) {
        RunAfterSeconds.Create(seconds, () => Respawn());
    }

    /// <summary>
    /// Respawn entity after waiting respawn time.
    /// Works even if you destroy this entity
    /// </summary>
    public void RespawnAfterRespawnTime () {
        RespawnAfterSeconds(_respawnTime);
    }

    /// <summary>
    /// Called every update. 
    /// Use this instead of regular Update
    /// </summary>  
    public virtual void UpdateEntity () {
        // Die if falling outside world
        if (transform.position.y < -100) {
            Die();
        }
    }

    public virtual void InflictDamage(int damage, Vector3 hitPosition, float knockbackAmount) {
        if (invincible) {
            return;
        }
        _characterController2D.isHit = true;

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

    public void Knockback(Vector3 hitPosition, float knockbackAmount) {
        Vector2 moveDirection = _rigidbody.transform.position - hitPosition;

        if(moveDirection == Vector2.zero) {
            moveDirection = Vector2.one;
        }
        
        _rigidbody.velocity = moveDirection.normalized * knockbackAmount;
    }

    public virtual IEnumerator OnHitEffects () {
        _renderer = GetComponentInChildren<Renderer>();

        Color defaultColor = _renderer.material.color;
        Color onHitColor = _onhitColor;

        onHitColor.a = defaultColor.a;
        _renderer.material.color = onHitColor;
        yield return new WaitForSeconds(ON_HIT_FLASH_TIME);
        _renderer.material.color = defaultColor;
    }

    public IEnumerator OnHitNoMove() {
        yield return new WaitForSeconds(0.3f);
        _characterController2D.isHit = false;
    }

    void Update () {
        UpdateEntity();
    }

}
