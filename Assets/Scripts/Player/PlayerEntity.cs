using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

    public Transform respawnPoint;

    public override void Respawn() {
        Instantiate(Resources.Load<GameObject>("Player1"), respawnPoint.position, Quaternion.identity);
    }

    public override void Die() {
        Destroy(gameObject);
        Respawn();
    }

}
