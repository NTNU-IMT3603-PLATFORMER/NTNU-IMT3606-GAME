using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerTransformationData contains all the data for each transformation.
/// This data can be set with the unity editor.
/// </summary>
[System.Serializable]
public class PlayerTransformationData {

    /// <summary>
    /// _transformationState is a field that holds a game object wich contains components that are responible for setting
    /// sprites, animations, and hitboxes for each transformation state.
    /// </summary>
    [SerializeField]
    GameObject _transformationState;

    [Header("CharacterController2D")]
    // Refer to CharacterController2D for variable descriptions.
    [SerializeField]
    float _jumpHeight;
    [SerializeField]
    int _extraAirJumps;
    [SerializeField]
    float _dashDistance;
    [SerializeField]
    float _dashSpeed;
    [SerializeField]
    int _maxDashes;
    [SerializeField]
    bool _enableWallSlide;
    [SerializeField]
    bool _enableWallJump;
    [SerializeField]
    bool _enableDashing;

    [Header("PlayerCombat")]
    // Refer to PlayerCombat for variable descriptions.
    [SerializeField]
    int _baseDamage;
    [SerializeField]
    float _attackRange;
    [SerializeField]
    float _attackCooldown;

    public float jumpHeight => _jumpHeight;
    public bool enableWallSlide => _enableWallSlide;
    public bool enableWallJump => _enableWallJump;
    public int extraAirJumps => _extraAirJumps;
    public bool enableDashing => _enableDashing;
    public float dashDistance => _dashDistance;
    public float dashSpeed => _dashSpeed;
    public int maxDashes => _maxDashes;
    public GameObject transformationState => _transformationState;


    public int baseDamage => _baseDamage;
    public float attackRange => _attackRange;
    public float attackCooldown => _attackCooldown;

}
