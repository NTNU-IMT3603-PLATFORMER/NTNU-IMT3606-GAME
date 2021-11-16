using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    [SerializeField]    string _name;
    [TextArea(3, 10)]
    [SerializeField]    string[] _sentences;


    public string name => _name;

    public string[] sentences => _sentences;

       
}

