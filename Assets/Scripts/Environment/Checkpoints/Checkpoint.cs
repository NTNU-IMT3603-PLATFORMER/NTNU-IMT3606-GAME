using UnityEngine;

/// <summary>
/// Class representing a single checkpoint
/// </summary>
[RequireComponent(typeof(EntityCollision))]
public class Checkpoint : MonoBehaviour {

    Checkpoints _checkpoints;

    void Start() {
        _checkpoints = GetComponentInParent<Checkpoints>();
        GetComponent<EntityCollision>().eventOnEntityCollisionEnter.AddListener(OnEntityCollisionEnter);
    }

    void OnEntityCollisionEnter(Entity entity) {
        if (entity == PlayerEntity.INSTANCE) {
            _checkpoints.OnCheckpointEnter(this);
        }
    }

}