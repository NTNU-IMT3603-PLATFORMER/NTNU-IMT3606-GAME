using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Entity : MonoBehaviour
{
    [SerializeField, Tooltip("The health of the entity")]
    int _health;

    public abstract void TakeDamage(int damage);
    public abstract void Respawn();
}
