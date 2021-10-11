using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public override void Respawn() {
    }

    public override void Die() {
        Debug.Log("Enemy died!");

        this.enabled = false;
    }
}
