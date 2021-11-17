using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]    Text _nameText;
    [SerializeField]    Text _dialogueText;

    Queue<string> _sentences;

    // Start is called before the first frame update
    void Start() {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        _nameText.text = dialogue.name;
        _sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public bool DisplayNextSentence() {
        if(_sentences.Count == 0) {
            EndDialogue();
            return true;
        }

        string sentence = _sentences.Dequeue();
        _dialogueText.text = sentence;
        return false;
    }

    void EndDialogue() {
        Debug.Log("End of conversation");
    }

    public void ResetDialogue(Dialogue dialogue) {
        _sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

    }
}
