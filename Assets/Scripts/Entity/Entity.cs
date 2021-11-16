using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour {

    [SerializeField, Tooltip("The rigidbody of the entity")]                        Rigidbody2D _rigidbody;
    [SerializeField, Tooltip("The health of the entity")]                           int _health = 10;
    [SerializeField, Tooltip("The max health of the entity")]                       int _maxHealth = 10;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")]     bool _invincible;
    [SerializeField, Tooltip("The color the entity will get when hit")]             Color _onhitColor;
    [SerializeField, Tooltip("Time to respawn after death")]                        float _respawnTime = 10f;
    [SerializeField, Tooltip("Should the entity respawn after death?")]             bool _shouldRespawn = true;
    public int spirits = 0;

    CharacterController2D _characterController2D;
    Renderer _renderer;

    void Awake() {
        _characterController2D = GetComponent<CharacterController2D>();
    }


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
    public bool shouldRespawn {
        get => _shouldRespawn;
        set => _shouldRespawn = value;
    }

    public abstract void Respawn();
    public abstract void Die();

    public virtual void AddBlood() {
       // Empty because only the PlayerEntity will need this function. 
    }


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
        // TODO: Find a better way to fetch the renderer
        _renderer = GetComponentInChildren<Renderer>();
        StartCoroutine(OnHitInvincibility());
        StartCoroutine(OnhitFlash());
        StartCoroutine(OnHitNoMove());
        if (knockbackAmount != 0f) {
            Knockback(hitPosition, knockbackAmount);
        }
    }

    public void Knockback(Vector3 hitPosition, float knockbackAmount) {
        Vector2 moveDirection = _rigidbody.transform.position - hitPosition;

        if(moveDirection == Vector2.zero) {
            moveDirection = Vector2.one;
        }
        
        _rigidbody.velocity = moveDirection.normalized * knockbackAmount;
    }

    public IEnumerator OnHitInvincibility() {
        invincible = true;
        for (var i = 0; i < 3; i++){
            _renderer.material.color = new Color(1f,1f,1f,.5f);
            yield return new WaitForSeconds(0.2f);
            _renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        invincible = false;
    }

    public IEnumerator OnhitFlash() {
        _renderer.material.color = _onhitColor;
        yield return new WaitForSeconds(0.3f);
        _renderer.material.color = Color.white;
    }

    public IEnumerator OnHitNoMove() {
        yield return new WaitForSeconds(0.3f);
        _characterController2D.isHit = false;
    }

    void Update () {
        UpdateEntity();
    }

}
