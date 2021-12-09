using UnityEngine;

[RequireComponent(typeof(EntityCollision))]
public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    Dialogue _dialogue;

    DialogueManager _dialogueManager;
    EntityCollision _entityCollision;
    Vector3 _npcPosition;
    bool _firstTalk;

    /// <summary>
    /// Ran when dialogue is triggered,
    /// checks if you are already in a conversation with the npc
    /// </summary>
    public void TriggerDialogue() {
        if (_firstTalk) {
            _dialogueManager.StartDialogue(_dialogue);
            _firstTalk = false;
        } else {
            _firstTalk = _dialogueManager.DisplayNextSentence();
        }
    }

    void Update() {
        if (Input.GetButtonDown("Talk")) {
            if (Vector3.Distance(PlayerEntity.INSTANCE.transform.position, _npcPosition) < 2) {
                TriggerDialogue();
            }
        }
    }

    void OnEntityTriggerExit(Entity entity) {
        _dialogueManager.EndDialogue();
        _firstTalk = true;
    }

    void Start() {
        _npcPosition = transform.position;
        _firstTalk = true;
        _dialogueManager = GetComponentInChildren<DialogueManager>();
        _entityCollision = GetComponent<EntityCollision>();
        _entityCollision.eventOnEntityCollisionExit.AddListener(OnEntityTriggerExit);
    }

}
