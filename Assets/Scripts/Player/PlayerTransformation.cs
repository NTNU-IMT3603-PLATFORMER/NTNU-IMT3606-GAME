using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerTransformation handles the logic behind when a player tranforms from one state to another in the game.
/// </summary>
public class PlayerTransformation : MonoBehaviour {

    CharacterController2D _characterController2D;
    Rigidbody2D _myBody;
    SpriteRenderer _spriteRenderer;

    public Transform hitBoxes;
    
    [SerializeField]
    PlayerTransformationData druidData;
    
    [SerializeField]
    PlayerTransformationData polarBearData;
    
    [SerializeField]
    PlayerTransformationData desertCatData;
    
    [SerializeField]
    PlayerTransformationData owlData;

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
    /// <param name="transformation">Is of type enum</param>
    public void TransformInto(Transformation transformation) {
        PlayerTransformationData data = null;
        switch (transformation) {
            case Transformation.Druid:
                data = druidData;
                break;
            case Transformation.PolarBear:
                data = polarBearData;
                break;
            case Transformation.DesertCat:
                data = desertCatData;
                break;
            case Transformation.Owl:
                data = owlData;
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
        
        foreach (Transform child in hitBoxes) {
            child.gameObject.SetActive(false);
        }
        
        data.hitBox.SetActive(true);
    }
        
   

    void Start() {
        _myBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterController2D = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
