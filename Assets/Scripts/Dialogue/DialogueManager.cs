using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    Queue<string> _sentences;

    // Start is called before the first frame update
    void Start() {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
    }

}
