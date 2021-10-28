using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : EntityCombat {

    AudioManager _audioManager;

     void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public override void UpdateCombat() {
        base.UpdateCombat();

        if (Input.GetButtonDown("Attack") && canAttack) {
            Attack(baseDamage);
            _audioManager.PlaySound("swordhit");
        }
    }

}