using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all the data for each transformation.
/// </summary>
[System.Serializable]
public class PlayerTransformationData {

    /// <summary>
    /// _transformationState is a field that holds a game object wich contains components that are responible for setting
    /// sprites, animations, and hitboxes for each transformation state.
    /// </summary>
    [SerializeField]
    GameObject _transformationState;

    // Refer to CharacterController2D for variable descriptions    
    [SerializeField]
    float _jumpHeight;

    [SerializeField]
    bool _enableWallSlide;

    [SerializeField]
    bool _enableWallJump;

    [SerializeField]
    int _extraAirJumps;

    [SerializeField]
    bool _enableDashing;

    [SerializeField]
    float _dashDistance;

    [SerializeField]
    float _dashSpeed;

    [SerializeField]
    int _maxDashes;

    public float jumpHeight => _jumpHeight;
    public bool enableWallSlide => _enableWallSlide;
    public bool enableWallJump => _enableWallJump;
    public int extraAirJumps => _extraAirJumps;
    public bool enableDashing => _enableDashing;
    public float dashDistance => _dashDistance;
    public float dashSpeed => _dashSpeed;
    public int maxDashes => _maxDashes;
    public GameObject transformationState => _transformationState;
}
  
