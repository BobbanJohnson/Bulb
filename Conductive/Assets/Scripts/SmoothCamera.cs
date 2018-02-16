using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

    [SerializeField] private bool smooth;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;

	void LateUpdate ()
    {
        Vector3 desiredPosition = lookAt.transform.position + offset;

        if (smooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
