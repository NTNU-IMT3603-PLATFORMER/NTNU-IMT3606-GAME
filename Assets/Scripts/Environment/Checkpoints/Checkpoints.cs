using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    Checkpoint[] _checkpoints;
    int _currentCheckpoint = -1;

    /// <summary>
    /// The checkpoint the player has reached and can respawn at
    /// </summary>
    public int currentCheckpoint => _currentCheckpoint;

    public void OnCheckpointEnter (Checkpoint checkpoint) {
        int index = Array.IndexOf(_checkpoints, checkpoint);

        if (currentCheckpoint < index) {
            _currentCheckpoint = index;
        }
    }

    void Start () {
        _checkpoints = GetComponentsInChildren<Checkpoint>();
    }

    #if UNITY_EDITOR
    void OnDrawGizmos () {
        UnityEditor.Handles.color = Color.white;
        
        Checkpoint[] checkpoints = GetComponentsInChildren<Checkpoint>();

        for (int i = 0; i < checkpoints.Length; i++) {
            UnityEditor.Handles.Label( checkpoints[i].transform.position, $"Checkpoint {i + 1}/{checkpoints.Length}");
        }
    }
    #endif

}