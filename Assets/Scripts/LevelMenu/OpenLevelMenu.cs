using UnityEngine;

public class OpenLevelMenu : MonoBehaviour {

    [SerializeField]
    GameObject levelMenu;

    bool _isAlreadyOpen;


    void Update() {
        float distanceToPlayer = Vector3.Distance(PlayerEntity.INSTANCE.transform.position, transform.position);
        if (Input.GetButtonDown("Talk")) {
            if (distanceToPlayer < 2 && _isAlreadyOpen == false) {
                //TODO: add something which indicates the npc is interactable
                openLevelMenu(false);
            } else if (distanceToPlayer < 2) {
                openLevelMenu(true);
            }
        }



        if (distanceToPlayer < 5 && distanceToPlayer > 2) {
            //TODO: add something which indicates the npc is interactable
            openLevelMenu(true);
        }
    }

    public void openLevelMenu(bool isOpen) {
        if (isOpen) {
            Debug.Log(":)");
            levelMenu.SetActive(false);
            _isAlreadyOpen = false;
        } else {
            Debug.Log("no bør du funk for faen");
            levelMenu.SetActive(true);
            GameObject.FindObjectOfType<IncreaseValues>(true).UpdateValues();
            _isAlreadyOpen = true;
        }

    }
}
