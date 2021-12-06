using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLevelMenu : MonoBehaviour
{

    [SerializeField]
    GameObject levelMenu;

    bool _isAlreadyOpen;

    
    void Update() {
        if (Input.GetButtonDown("Talk")) {
            if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, transform.position) < 2 && _isAlreadyOpen == false) {
                //TODO: add something which indicates the npc is interactable
                openLevelMenu(false);
            } else {
                openLevelMenu(true);
            } 
        }

        float distanceToPlayer = Vector3.Distance(PlayerEntity.INSTANCE.transform.position, transform.position);

        if (distanceToPlayer < 5 && distanceToPlayer > 2) {
            //TODO: add something which indicates the npc is interactable
            openLevelMenu(true);
        }
    }

    public void openLevelMenu(bool isOpen) {
        if(isOpen) {
            levelMenu.SetActive(false);
            _isAlreadyOpen = false;
        } else {
            levelMenu.SetActive(true);
            _isAlreadyOpen = true;
        }
        
    }
}
