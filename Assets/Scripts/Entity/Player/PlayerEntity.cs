using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    /// <summary>
    /// Global instance of PlayerEntity
    /// Use this for reference, as normal
    /// references will be destroyed when 
    /// player dies!
    /// </summary>
    public static PlayerEntity INSTANCE { get; private set; }

    const string RESPAWN_TAG = "PlayerRespawnPoint";

    Transform _respawnPoint;
    Cinemachine.CinemachineVirtualCamera _playerCamera;

    void Start () {
        INSTANCE = this;
        _respawnPoint = GameObject.FindWithTag(RESPAWN_TAG)?.transform;

        if (_respawnPoint == null) {
            Debug.LogError($"Respawn point not found! Create a new empty gameobject and assign it tag '{RESPAWN_TAG}' and make sure it is not disabled!");
        }

        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponentInParent<Cinemachine.CinemachineVirtualCamera>();
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

}
