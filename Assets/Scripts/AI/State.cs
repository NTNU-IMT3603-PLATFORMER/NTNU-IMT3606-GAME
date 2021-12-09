using UnityEngine;

public abstract class State : MonoBehaviour {

    [SerializeField, Tooltip("If this is checked, this state will be the start state of FSM")] bool _isStartState;

    /// <summary>
    /// Is true if it should be the first state running in FSM
    /// </summary>
    public bool isStartState => _isStartState;

    protected FSM fsm { get; private set; }

    /// <summary>
    /// Will get called by FSM every frame when running
    /// </summary>
    public virtual void OnUpdateState() { }

    /// <summary>
    /// Will get called by FSM when changing to this state
    /// </summary>
    public virtual void OnEnterState() { }

    /// <summary>
    /// Will get called by FSM when changing to another state
    /// </summary>
    public virtual void OnExitState() { }

    /// <summary>
    /// Will get called by FSM every FixedUpdate when running
    /// </summary>
    public virtual void OnFixedUpdateState() { }

    void Awake() {
        Initialize();
    }

    protected virtual void Initialize() {
        fsm = GetComponentInParent<FSM>();
    }
}

/// <summary>
/// State that can use T as a (optionally shared) data class for storing state information
/// </summary>
public abstract class State<T> : State where T : MonoBehaviour {

    protected T data { get; private set; }

    protected override void Initialize() {
        base.Initialize();
        data = GetComponentInParent<T>();

        // Check if required data script does not exist
        if (data == null) {
            Debug.LogError($"'{name}' gameobject or any of its parents do not have required '{typeof(T).Name}' component attached to it. Please add it", this);
        }
    }
}