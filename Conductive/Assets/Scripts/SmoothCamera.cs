using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    [SerializeField] float _smoothSpeed;
    [SerializeField] Transform _lookAt;
    [SerializeField] Vector3 _offset;


    void Awake()
    {
        PlayerManager.instance.OnTargetPlayerChanged += SetFollowTarget;
    }


    void LateUpdate()
    {
        Vector3 desiredPosition = _lookAt.transform.position + _offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
    }
    

    void SetFollowTarget(PlayerMovement inPlayer)
    {
        _lookAt = inPlayer.transform;
    }
}   
