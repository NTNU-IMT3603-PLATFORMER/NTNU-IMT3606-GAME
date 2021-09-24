using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public float smoothFactor = 0.1f;
    public Transform target;

    private Vector3 targetPosition;
    private Vector3 positionV;

    void Update () {
        targetPosition = new Vector3(target.position.x, target.position.y, -10f);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionV, smoothFactor);
    }

}
