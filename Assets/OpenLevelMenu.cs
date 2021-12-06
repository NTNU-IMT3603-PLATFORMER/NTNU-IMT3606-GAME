using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelMenu : MonoBehaviour
{

    public GameObject levelMenu;
    public Vector3 _fireplacePosition;


    void Start() {
        _fireplacePosition = transform.position;
    }
    void Update() {
        if (Input.GetButtonDown("Talk")) {
            if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _fireplacePosition) < 2) {
                //TODO: add something which indicates the npc is interactable
                openLevelMenu(false);
            }
        }
        if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _fireplacePosition) > 2) {
            //TODO: add something which indicates the npc is interactable
            openLevelMenu(true);
        }


    }

    public void openLevelMenu(bool isOpen) {
        if(isOpen) {
            levelMenu.SetActive(false);
        } else {
            levelMenu.SetActive(true);
        }
        
    }
}
