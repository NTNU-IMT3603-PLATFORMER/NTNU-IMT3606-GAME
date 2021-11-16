using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
    public Dialogue dialogue;

    Vector3 _npcPosition;


    private void Start() {
        _npcPosition = transform.position;
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Update() {
        if(Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _npcPosition) < 3) {
            //TODO: add something which indicates the npc is interactable
            if(Input.GetButtonDown("Talk")) {
                TriggerDialogue();
            }
        }
    }
}
