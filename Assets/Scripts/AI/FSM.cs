using System.Linq;
using UnityEngine;

/// <summary>Finite State Machine</summary>
public class FSM : MonoBehaviour {

    const string CHILD_NAME = "States";

    Transform statesChild;
    State[] _states;
    State _currentState;
    bool _isChangingState;

    /// <summary>
    /// Will change from the current to the given state.
    /// Will call OnExitState on current state.
    /// Will call OnEnterState on the target state
    /// </summary>
    public void ChangeState<T>() where T : State {
        // Check if ChangeState was called from OnExitState
        // (which is not allowed because it can lead to hard-to-debug bugs)
        if (_isChangingState) {
            Debug.LogError($"FSM on {name}: A state tried changing to another state in OnExitState and this is not allowed", statesChild);
            return;
        }

        _isChangingState = true;
        _currentState.OnExitState();

        // Find state script based on type
        // Uses "AssemblyQualifiedName" for comparison because comparing
        // System.Type is not reliable
        State newState = _states.FirstOrDefault(state => state.GetType().AssemblyQualifiedName.Equals(typeof(T).AssemblyQualifiedName));

        if (newState == null) {
            Debug.LogError($"FSM on {name} tried changing to missing state '{typeof(T).Name}'. Make sure this state exists", statesChild);
            return;
        }

        _isChangingState = false;
        _currentState = newState;
        _currentState.OnEnterState();
    }

    void Awake() {
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

    void Start() {
        if (_currentState != null) {
            _currentState.OnEnterState();
        }
    }

    void Update() {
        if (_currentState != null) {
            _currentState.OnUpdateState();
        }
    }

    void FixedUpdate() {
        if (_currentState != null) {
            _currentState.OnFixedUpdateState();
        }
    }
}