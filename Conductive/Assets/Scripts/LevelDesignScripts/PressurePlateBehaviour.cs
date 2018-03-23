using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    bool _isBeingPressed;
    public bool isBeingPressed { get { return _isBeingPressed; } }

    void OnTriggerEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Play plate animation and sound");
            _isBeingPressed = true;
        }
    }

    void OnTriggerExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Play plate restore animation and sound");
            _isBeingPressed = false;
        }
    }
}
