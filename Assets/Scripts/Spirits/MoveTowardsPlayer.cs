using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {

    [SerializeField]    float _speed = 5f;
    [SerializeField]    float _distanceToFollowPlayer = 10f;

    void Update () {
        if (transform.position.IsWithinDistanceOf(PlayerEntity.INSTANCE.transform.position, _distanceToFollowPlayer)) {
            transform.position = Vector3.MoveTowards(transform.position, PlayerEntity.INSTANCE.transform.position, _speed * Time.deltaTime);
        }
    }

}
