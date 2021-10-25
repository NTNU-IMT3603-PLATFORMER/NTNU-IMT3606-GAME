using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper behaviour that will run a function
/// after a given amount of seconds after Start.
/// Useful when you have to destroy gameobject,
/// but still want to carry out an action after a
/// certain amount of seconds
/// </summary>
public class RunAfterSeconds : MonoBehaviour {

    public float secondsToWait { get; private set; }
    public System.Action action { get; private set; }

    void Start () {
        StartCoroutine(RunWhenReady());
    }

    IEnumerator RunWhenReady () {
        yield return new WaitForSeconds(secondsToWait);
        action();
        Destroy(gameObject);
    }

    /// <summary>
    /// Create a gameobject that will carry out given action
    /// after a certain amount of seconds
    /// </summary>
    public static void Create (float secondsToWait, System.Action action) {
        GameObject obj = new GameObject("RunAfterSecondsJob");
        RunAfterSeconds script = obj.AddComponent<RunAfterSeconds>();

        script.secondsToWait = secondsToWait;
        script.action = action;
    }

}