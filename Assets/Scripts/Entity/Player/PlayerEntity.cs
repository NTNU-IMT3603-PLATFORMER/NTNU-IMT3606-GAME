using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    Transform _respawnPoint;
    Cinemachine.CinemachineVirtualCamera _playerCamera;

    void Start () {
        _respawnPoint = GameObject.FindWithTag("PlayerRespawnPoint").transform;
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
