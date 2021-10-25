using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Entity : MonoBehaviour {

    [SerializeField, Tooltip("The rigidbody of the entity")]                        Rigidbody2D _rigidbody;
    [SerializeField, Tooltip("The health of the entity")]                           int _health;
    [SerializeField, Tooltip("If true, inflicting damage will have no effect")]     bool _invincible;
    [SerializeField, Tooltip("The color the entity will get when hit")]             Color _onhitColor;

    CharacterController2D _characterController2D;
    Renderer _renderer;

    public void Awake() {
        _characterController2D = GetComponent<CharacterController2D>();
        _renderer = GetComponentInChildren<Renderer>();
    }

    /// <summary>
    /// The health of the entity
    /// </summary>
    public int health => _health;
    
    /// <summary>
    /// If true, inflicting damage will have no effect
    /// </summary>
    public bool invincible {
        get => _invincible;
        set => _invincible = value;
    }

    public abstract void Respawn();
    public abstract void Die();

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

    public virtual void InflictDamage(int damage, Transform opponentTransform, float knockbackAmount) {
        if (invincible) {
            return;
        }
        _characterController2D.isHit = true;

        _health -= damage;

        if (_health <= 0) {
            Die();
        }

        StartCoroutine(onhitFlash());
        StartCoroutine(onHitNoMove());
        if (knockbackAmount != 0f) {
            Knockback(opponentTransform, knockbackAmount);
        }
    }

    public void Knockback(Transform opponentTransform, float knockbackAmount) {
        Vector2 moveDirection = _rigidbody.transform.position - opponentTransform.transform.position;

        if(moveDirection == Vector2.zero) {
            moveDirection = Vector2.one;
        }
        Debug.Log(moveDirection.normalized);
        _rigidbody.velocity = moveDirection.normalized * knockbackAmount;
    }

    public IEnumerator onhitFlash() {
        _renderer.material.color = _onhitColor;
        yield return new WaitForSeconds(0.3f);
        _renderer.material.color = Color.white;
    }

    public IEnumerator onHitNoMove() {
        yield return new WaitForSeconds(0.3f);
        _characterController2D.isHit = false;
    }
    
    void Update () {
        UpdateEntity();
    }

}
