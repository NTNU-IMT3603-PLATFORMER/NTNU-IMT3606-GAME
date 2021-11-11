using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{

    public Transform Target;
    Vector3 _velocity = Vector3.zero;
    [SerializeField]
    float _travelTime;
    bool _isFollowing = false;

    // Update is called once per frame

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    void Update()
    {
        if(_isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * _travelTime);

        }
    }
}
