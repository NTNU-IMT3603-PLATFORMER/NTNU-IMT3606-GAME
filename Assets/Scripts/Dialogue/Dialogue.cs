using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    [SerializeField]                    string _name;
    [SerializeField, TextArea(3, 10)]   string[] _sentences;


    public string name => _name;

    public string[] sentences => _sentences;

       
}

