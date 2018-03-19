using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    [SerializeField] bool _isBeingPressed;

    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            if (_isBeingPressed != true)
            {
                Debug.Log("Play plate animation and sound");
                _isBeingPressed = true;
            }
            else
                return;
    }

    void OnTriggerExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
            if (_isBeingPressed != false)
            {
                Debug.Log("Play plate restore animation and sound");
                _isBeingPressed = false;
            }
            else
                return;
    }

    public bool GetPlateState()
    {
        return _isBeingPressed;
    }
}
