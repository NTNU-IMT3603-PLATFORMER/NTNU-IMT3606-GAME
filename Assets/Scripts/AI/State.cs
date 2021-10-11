using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

    [SerializeField, Tooltip("If this is checked, this state will be the start state of FSM")] bool _isStartState;

    public bool isStartState => _isStartState;

    protected FSM fsm { get; private set; }

    public abstract void OnEnterState();
    public abstract void OnUpdateState ();
    public abstract void OnExitState();

    void Awake () {
        fsm = GetComponentInParent<FSM>();
    }

}
