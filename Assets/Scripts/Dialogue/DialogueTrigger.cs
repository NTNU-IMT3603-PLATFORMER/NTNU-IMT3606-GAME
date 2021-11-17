using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{
    public Dialogue dialogue;
    Vector3 _npcPosition;
    bool _firstTalk;
    DialogueManager _dialogueManager;

    private void Start() {
        _npcPosition = transform.position;
        _firstTalk = true;
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue ()
    {
        if(_firstTalk) {
            _dialogueManager.StartDialogue(dialogue);
            _firstTalk = false;
        } else {
            _firstTalk = _dialogueManager.DisplayNextSentence();
        }
    }

    void Update() {
        if (Input.GetButtonUp("Talk")) {
            if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _npcPosition) < 2) {
                //TODO: add something which indicates the npc is interactable
                TriggerDialogue();
            } 
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        _dialogueManager.EndDialogue();
        _firstTalk = true;
    }
}
