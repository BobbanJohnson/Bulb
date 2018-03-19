using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] bool _isPressed = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if (_isPressed != true)
            {
                Debug.Log("Play button animation and sound");
                _isPressed = true;
            }
            else
                Debug.Log("This bad boy has been pressed");
    }

    public bool GetButtonState()
    {
        return _isPressed;
    }
}
