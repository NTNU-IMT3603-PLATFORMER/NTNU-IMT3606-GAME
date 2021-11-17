using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]    Text _nameText;
    [SerializeField]    Text _dialogueText;

    Canvas _textBoxCanvas;

    Queue<string> _sentences;

    // Start is called before the first frame update
    void Start() {
        _sentences = new Queue<string>();
        _textBoxCanvas = transform.parent.GetComponentInChildren<Canvas>();
        _textBoxCanvas.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue) {
        _nameText.text = dialogue.name;
        _sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        _textBoxCanvas.enabled = true;
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

    public void EndDialogue() {
        Debug.Log("End of conversation");
        _sentences.Clear();
        _textBoxCanvas.enabled = false;
    }
}
