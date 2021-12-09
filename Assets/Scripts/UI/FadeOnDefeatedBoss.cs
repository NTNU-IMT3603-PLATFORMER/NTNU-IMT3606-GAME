using UnityEngine;

/// <summary>
/// Activates fading when boss is defeated
/// </summary>
[RequireComponent(typeof(FadePanel))]
public class FadeOnDefeatedBoss : MonoBehaviour {

    FadePanel _fadePanel;

    void Start() {
        _fadePanel = GetComponent<FadePanel>();

        if (BossDefeatedHandler.INSTANCE != null) {
            BossDefeatedHandler.INSTANCE.eventOnDefeatedBoss.AddListener(() => _fadePanel.fadeTarget = 1f);
        }
    }

}