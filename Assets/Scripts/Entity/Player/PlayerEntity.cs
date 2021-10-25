using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    const string RESPAWN_TAG = "PlayerRespawnPoint";

    Transform _respawnPoint;
    Cinemachine.CinemachineVirtualCamera _playerCamera;

    void Start () {
        _respawnPoint = GameObject.FindWithTag(RESPAWN_TAG)?.transform;

        if (_respawnPoint == null) {
            Debug.LogError($"Respawn point not found! Create a new empty gameobject and assign it tag '{RESPAWN_TAG}' and make sure it is not disabled!");
        }

        _playerCamera = GameObject.FindWithTag("MainCamera").GetComponentInParent<Cinemachine.CinemachineVirtualCamera>();
    }

    public override void Respawn() {
        GameObject newPlayer = Instantiate(Resources.Load<GameObject>("Player"), _respawnPoint.position, Quaternion.identity);
        _playerCamera.Follow = newPlayer.transform;
    }

    public override void Die() {
        Destroy(gameObject);
        Respawn();
    }

}
