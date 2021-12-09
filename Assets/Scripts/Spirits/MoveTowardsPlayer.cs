using UnityEngine;


/// <summary>
/// This script makes the spirit that the enemies drop move towards the player. 
/// </summary>
public class MoveTowardsPlayer : MonoBehaviour {

    [SerializeField]
    float _speed = 5f;
    [SerializeField]
    float _distanceToFollowPlayer = 10f;

    void Update() {
        if (transform.position.IsWithinDistanceOf(PlayerEntity.INSTANCE.transform.position, _distanceToFollowPlayer)) {
            transform.position = Vector3.MoveTowards(transform.position, PlayerEntity.INSTANCE.transform.position, _speed * Time.deltaTime);
        }
    }

}
