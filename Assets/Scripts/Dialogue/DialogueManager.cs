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

        _sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }
    }

    public bool DisplayNextSentence() {
        if(_sentences.Count == 0) {
            EndDialogue();
            return true;
        }

        string sentence = _sentences.Dequeue();
        Debug.Log(sentence);
        return false;
    }

    void EndDialogue() {
        Debug.Log("End of conversation");
    }
}
