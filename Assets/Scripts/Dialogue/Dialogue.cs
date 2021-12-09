using UnityEngine;

[System.Serializable]
public class Dialogue {
    [SerializeField]
    string _name;
    [SerializeField, TextArea(3, 10)]
    string[] _sentences;

    /// <summary>
    /// The name of the npc talking
    /// </summary>
    public string name => _name;

    /// <summary>
    /// An array of sentences the npc is saying
    /// </summary>
    public string[] sentences => _sentences;

}

