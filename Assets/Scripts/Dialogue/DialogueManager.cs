using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField]
    Text _nameText;
    [SerializeField]
    Text _dialogueText;
    [SerializeField]
    GameObject _dialoguePanel;

    Canvas _alertCanvas;

    Queue<string> _sentences;

    /// <summary>
    /// Enqueues sentence in a queue and shows the first sentence
    /// </summary>
    /// <param name="dialogue">A dialogue object which contains a name and sentences</param>
    public void StartDialogue(Dialogue dialogue) {
        _nameText.text = dialogue.name;
        _sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        _alertCanvas.enabled = false;
        _dialoguePanel.SetActive(true);
    }

    /// <summary>
    /// Displays the next sentence in the queue
    /// </summary>
    /// <returns>Returns true if no more sentences, false otherwise</returns>
    public bool DisplayNextSentence() {
        if (_sentences.Count == 0) {
            EndDialogue();
            return true;
        }

        string sentence = _sentences.Dequeue();
        _dialogueText.text = sentence;
        return false;
    }

    /// <summary>
    /// Clears the sentence queue and disables dialogue canvas and enables alert canvas
    /// </summary>
    public void EndDialogue() {
        _sentences.Clear();
        _dialoguePanel.SetActive(false);
        _alertCanvas.enabled = true;
    }

    // Start is called before the first frame update
    void Start() {
        _sentences = new Queue<string>();
        _alertCanvas = transform.parent.GetChild(3).GetComponent<Canvas>();
        _dialoguePanel.SetActive(false);
    }

}
