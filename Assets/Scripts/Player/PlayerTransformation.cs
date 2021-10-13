using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour {

    CharacterController2D _characterController2D;
    Rigidbody2D _myBody;
    SpriteRenderer _spriteRenderer;

    public Transform hitBoxes;

    public PlayerTransformationData druidData;
    public PlayerTransformationData polarBearData;
    public PlayerTransformationData desertCatData;
    public PlayerTransformationData owlData;

    public enum Transformation {
        Druid,
        PolarBear,
        DesertCat,
        Owl
    }

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
        if (Input.GetButtonDown("TransformIntoDessertCat")) {
            TransformInto(Transformation.DesertCat);
        }
        if (Input.GetButtonDown("TransformIntoOwl")) {
            TransformInto(Transformation.Owl);
        }
    }
}
