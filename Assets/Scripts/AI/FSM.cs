using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Finite State Machine</summary>
public class FSM : MonoBehaviour {

    const string CHILD_NAME = "States";

    Transform statesChild;
    State[] _states;
    State _currentState;

    void Awake () {
        statesChild = transform.Find(CHILD_NAME);

        // Error checking to see if FSM is properly setup

        if (statesChild == null) {
            Debug.LogError($"FSM on {name} requires there to be a child gameobject named '{CHILD_NAME}' with states attached", this);
            return;
        }

        _states = statesChild.GetComponents<State>();

        if (_states.Length == 0) {
            Debug.LogError($"FSM on {name} requires that there is at least 1 state attached to '{CHILD_NAME}' gameobject", statesChild);
            return;
        }
 
        State[] statesMarkedStartState = _states.Where(state => state.isStartState).ToArray();

        if (statesMarkedStartState.Length != 1) {
            Debug.LogError($"FSM on {name} requires that there is 1 (and only 1) state marked as start state", statesChild);
            return;
        }

        // Set start state
        _currentState = statesMarkedStartState[0];
    }

    void Start () {
        if (_currentState != null) {
            _currentState.OnEnterState();
        }
    }

    void Update () {
        if (_currentState != null) {
            _currentState.OnUpdateState();
        }
    }

    void FixedUpdate () {
        if (_currentState != null) {
            _currentState.OnFixedUpdateState();
        }
    }

    public void ChangeState<T> () where T : State {
        _currentState.OnExitState();

        State newState = _states.FirstOrDefault(state => state.GetType().AssemblyQualifiedName.Equals(typeof(T).AssemblyQualifiedName));

        if (newState == null) {
            Debug.LogError($"FSM on {name} tried changing to missing state '{typeof(T).Name}'. Make sure this state exists", statesChild);
            return;
        }

        _currentState = newState;
        _currentState.OnEnterState();
    }

}