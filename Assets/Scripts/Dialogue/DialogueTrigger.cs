using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
    public Dialogue dialogue;
    Vector3 _npcPosition;
    bool _firstTalk;

    private void Start() {
        _npcPosition = transform.position;
        _firstTalk = true;
    }

    public void TriggerDialogue ()
    {
        if(_firstTalk) {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            _firstTalk = false;
        } else {
            _firstTalk = FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    void Update() {
        if(Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _npcPosition) < 3) {
            //TODO: add something which indicates the npc is interactable
            if(Input.GetButtonUp("Talk")) {
                TriggerDialogue();
            }
        }
    }
}
