using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelMenu : MonoBehaviour
{

    [SerializeField]
    GameObject levelMenu;

    bool isAlreadyOpen;

    
    void Update() {
        if (Input.GetButtonDown("Talk")) {
            if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, transform.position) < 2 && isAlreadyOpen == false) {
                //TODO: add something which indicates the npc is interactable
                openLevelMenu(false);
            } else {
                openLevelMenu(true);
            } 
        }
        
        if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, transform.position) > 2) {
            //TODO: add something which indicates the npc is interactable
            openLevelMenu(true);
        }


    }

    public void openLevelMenu(bool isOpen) {
        if(isOpen) {
            levelMenu.SetActive(false);
            isAlreadyOpen = false;
        } else {
            levelMenu.SetActive(true);
            isAlreadyOpen = true;
        }
        
    }
}
