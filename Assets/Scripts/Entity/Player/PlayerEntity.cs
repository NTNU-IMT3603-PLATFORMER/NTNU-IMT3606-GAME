using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEntity : Entity {


    [SerializeField, Tooltip("Amount of time it takes for the player to heal")]         float _healTime = 1.141f;
    [SerializeField, Tooltip("Max amount of blood the entity can have")]                int _maxBloodLevel = 9;
    [SerializeField, Tooltip("Time after taking damage where player is invincible")]    float onHitInvincibilityTime = 1.2f;
    [SerializeField, Tooltip("Amount of flashes for on hit invincibility effect")]      int onHitInvincibilityFlashAmount = 3;

    int _bloodLevel = 0;
    float _startTime = 0f;
    float _currentPosition;
    int _spirits = 0;

    /// <summary>
    /// The blood level of the player
    /// </summary>
    public int bloodlevel => _bloodLevel;

    /// <summary>
    /// Global instance of PlayerEntity
    /// Use this for reference, as normal
    /// references will be destroyed when 
    /// player dies!
    /// </summary>
    public static PlayerEntity INSTANCE { get; private set; }

    const string RESPAWN_TAG = "PlayerRespawnPoint";

    CharacterController2D _characterController2D;
    Transform _respawnPoint;
    Cinemachine.CinemachineVirtualCamera _playerCamera;

    /// <summary>
    /// Current amount of spirits the player has absorbed
    /// </summary>
    public int spirits {
        get => _spirits;
        set => _spirits = value;
    }

    void Start () {
        _characterController2D = GetComponent<CharacterController2D>();
        INSTANCE = this;
        _respawnPoint = GameObject.FindWithTag(RESPAWN_TAG)?.transform;

        if (_respawnPoint == null) {
            Debug.LogError($"Respawn point not found! Create a new empty gameobject and assign it tag '{RESPAWN_TAG}' and make sure it is not disabled!");
        }

        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponentInParent<Cinemachine.CinemachineVirtualCamera>();
        
        Checkpoints.INSTANCE.eventOnReachedCheckpoint.AddListener(OnReachedCheckpoint);
    }

    public override void Respawn() {
        INSTANCE = this;

        GameObject newPlayer = Instantiate(Resources.Load<GameObject>("Player"), _respawnPoint.position.ToVec2(), Quaternion.identity);
        _playerCamera.Follow = newPlayer.transform;
    }

    public override void Die() {
        Destroy(gameObject);
        Respawn();
    }

    public void Heal() {
        if (_bloodLevel >= 3 && health < maxHealth) {
            health++;
            _bloodLevel -= 3;
        }
    }

    public override void AddBlood() {
        if(_bloodLevel < _maxBloodLevel) {
            _bloodLevel++;
        }
    }

    public override void UpdateEntity () {
        // Die if falling outside world
        if (transform.position.y < -100) {
            Die();
        }
        if (Input.GetButtonDown("Heal")) {
            _startTime = Time.time;
            _currentPosition = (float) Math.Round(transform.position.x, 1);
        }

        // TODO: Play a healing animation/use some effects while the healing button is held down
        if (Input.GetButton("Heal")){
            // Abort healing if the character is hit, is moving, or is not on the ground
            if (_characterController2D.isHit
                || !_characterController2D.isGrounded
                || (_currentPosition != Math.Round(transform.position.x,1))
            ){
                _startTime = Time.time;
                _currentPosition = (float) Math.Round(transform.position.x,1);
                return;
            }   
            
            if (_startTime + _healTime <= Time.time){
                Heal();
                _startTime = Time.time;
            }
            return;
        }
    }

    public override IEnumerator OnHitEffects() {
        invincible = true;

        // Perform base Entity OnHitEffects first before player-specific effects
        yield return base.OnHitEffects();

        // TODO: Find a better way to set the renderer when transforming.
        _renderer = GetComponentInChildren<Renderer>();

        // Subtract time spent on Entity base effects
        float timeLeft = onHitInvincibilityTime - ON_HIT_FLASH_TIME;

        // Calculate time that should be spent on each flash
        float timePerFlash = timeLeft / onHitInvincibilityFlashAmount;

        for (var i = 0; i < onHitInvincibilityFlashAmount; i++){
            Color defaultColor = Color.white;
            Color invincibleColor = defaultColor;
            invincibleColor.a = 0.5f;
            
            // Set color to invincibility color
            _renderer.material.color = invincibleColor;
            yield return new WaitForSeconds(timePerFlash / 2f);

            // Set color to default color
            _renderer.material.color = defaultColor;
            yield return new WaitForSeconds(timePerFlash / 2f);
        }

        invincible = false;
    }

    void OnReachedCheckpoint (Checkpoint checkpoint) {
        // Update respawn point to checkpoint
        _respawnPoint.transform.position = checkpoint.transform.position;
    }
    
}
