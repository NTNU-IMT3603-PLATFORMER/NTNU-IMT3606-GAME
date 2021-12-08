using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// PlayerTransformation handles the logic behind when a player tranforms from one state to another in the game.
/// </summary>
public class PlayerTransformation : MonoBehaviour {

    CharacterController2D _characterController2D;
    PlayerCombat _playerCombat;

    public Transform Transformations;

    [SerializeField]
    PlayerTransformationData _druidData;

    [SerializeField]
    PlayerTransformationData _polarBearData;

    [SerializeField]
    PlayerTransformationData _desertCatData;

    [SerializeField]
    PlayerTransformationData _owlData;

    /// <summary>
    /// Transformation contains the different transformation that the player is capable of doing.
    /// </summary>
    public enum Transformation {
        Druid,
        PolarBear,
        DesertCat,
        Owl
    }

    /// <summary>
    /// TransformInto takes a transformation form as an input and sets the CharacterController2D attributes based on the corresponding PlayerTransformationData class fields
    /// </summary>
    /// <param name="transformation">Is of type enum and contains the different transformations.</param>
    public void TransformInto(Transformation transformation) {
        PlayerTransformationData data = null;
        switch (transformation) {
            case Transformation.Druid:
                data = _druidData;
                break;
            case Transformation.PolarBear:
                data = _polarBearData;
                AudioManager.instance.PlaySound("beartransformation");
                break;
            case Transformation.DesertCat:
                data = _desertCatData;
                AudioManager.instance.PlaySound("cattransformation");
                break;
            case Transformation.Owl:
                data = _owlData;
                AudioManager.instance.PlaySound("owltransformation");
                break;
        }
        _characterController2D.jumpHeight = data.jumpHeight;

        _characterController2D.enableWallSlide = data.enableWallSlide;

        _characterController2D.enableWallJump = data.enableWallJump;

        _characterController2D.extraAirJumps = data.extraAirJumps;

        _characterController2D.enableDashing = data.enableDashing;

        _characterController2D.dashDistance = data.dashDistance;

        _characterController2D.dashSpeed = data.dashSpeed;

        _characterController2D.maxDashes = data.maxDashes;

        _playerCombat.baseDamage = data.baseDamage;

        _playerCombat.attackRange = data.attackRange;

        _playerCombat.attackCooldown = data.attackCooldown;

        DisableAllTransformationObjects();

        data.transformationState.SetActive(true);
    }

    void DisableAllTransformationObjects() {
        foreach (Transform child in Transformations) {
            child.gameObject.SetActive(false);
        }
    }

    void Start() {
        _characterController2D = GetComponent<CharacterController2D>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    void Update() {
        if (Input.GetButtonDown("TransformIntoDruid")) {
            TransformInto(Transformation.Druid);
        }
        if (Input.GetButtonDown("TransformIntoPolarBear")) {
            TransformInto(Transformation.PolarBear);
        }
        if (Input.GetButtonDown("TransformIntoDesertCat")) {
            TransformInto(Transformation.DesertCat);
        }
        if (Input.GetButtonDown("TransformIntoOwl")) {
            TransformInto(Transformation.Owl);
        }
    }

}
