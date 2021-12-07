using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class controlling checkpoints and 
/// checkpoint behaviour in general
/// </summary>
public class Checkpoints : MonoBehaviour {

    /// <summary>
    /// Global instance of Checkpoints
    /// </summary>
    public static Checkpoints INSTANCE { get; private set; }

    Checkpoint[] _checkpoints;
    int _currentCheckpoint = -1;
    UnityEvent<Checkpoint> _eventOnReachedCheckpoint = new UnityEvent<Checkpoint>();

    /// <summary>
    /// Event for when player has reached a new checkpoint
    /// </summary>
    public UnityEvent<Checkpoint> eventOnReachedCheckpoint => _eventOnReachedCheckpoint;

    /// <summary>
    /// The checkpoint the player has reached and can respawn at.
    /// Returns -1 if no checkpoints have been reached
    /// </summary>
    public int currentCheckpoint => _currentCheckpoint;

    /// <summary>
    /// Gets called by each checkpoint when player
    /// reaches them
    /// </summary>
    public void OnCheckpointEnter(Checkpoint checkpoint) {
        int index = Array.IndexOf(_checkpoints, checkpoint);

        if (currentCheckpoint < index) {
            _currentCheckpoint = index;
            _eventOnReachedCheckpoint.Invoke(checkpoint);
        }
    }

    void Awake() {
        INSTANCE = this;
        _checkpoints = GetComponentsInChildren<Checkpoint>();
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        UnityEditor.Handles.color = Color.white;

        Checkpoint[] checkpoints = GetComponentsInChildren<Checkpoint>();

        // Draws where checkpoint is in the list of checkpoints
        for (int i = 0; i < checkpoints.Length; i++) {
            UnityEditor.Handles.Label(checkpoints[i].transform.position, $"Checkpoint {i + 1}/{checkpoints.Length}");
        }
    }
#endif

}