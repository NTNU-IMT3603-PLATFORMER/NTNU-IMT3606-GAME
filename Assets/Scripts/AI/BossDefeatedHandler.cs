using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles what happens when you defeat a boss
/// </summary>
public class BossDefeatedHandler : MonoBehaviour {

    const float TIME_TO_LOAD_NEXT_LEVEL = 5f;

    [SerializeField] 
    EnemyEntity _boss;
    [SerializeField] 
    string _nextLevelName;

    UnityEvent _eventOnDefeatedBoss = new UnityEvent();
    bool _defeatedBoss;

    /// <summary>
    /// Returns the instance of BossDefeatedHandler
    /// There should only be 1 BossDefeatedHandler at any given time
    /// </summary>
    public static BossDefeatedHandler INSTANCE { get; private set; }

    /// <summary>
    /// Event triggered by defeating boss on current level
    /// </summary>
    public UnityEvent eventOnDefeatedBoss => _eventOnDefeatedBoss;

    /// <summary>
    /// Have we defeated the boss on the current level?
    /// </summary>
    public bool defeatedBoss => _defeatedBoss;

    void Awake () {
        INSTANCE = this;

        _boss.eventOnDeath.AddListener(() => StartCoroutine(OnDeath()));
    }

    IEnumerator OnDeath () {
        _defeatedBoss = true;
        _eventOnDefeatedBoss.Invoke();
        yield return new WaitForSeconds(TIME_TO_LOAD_NEXT_LEVEL);
        SceneManager.LoadScene(_nextLevelName);
    }

}