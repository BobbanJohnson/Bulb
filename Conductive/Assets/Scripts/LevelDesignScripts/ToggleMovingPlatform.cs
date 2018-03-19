using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMovingPlatform : MonoBehaviour
{
    [Header("Platform trigger")]
    [SerializeField] ButtonBehaviour _button;
    [SerializeField] PressurePlateBehaviour _pressurePlate;

    [Header("Platform variables")]
    [SerializeField] Transform _targetTransform; // Insert a gameobject reference here
    [SerializeField] float _platformMoveTime;

    bool _isMoving = false;
    bool _movedToTarget = false;
    Vector3 _startPos;

    void Awake()
    {
        // Only one button or one pressure plate can be connected; nullify the other
        if (_button != null)
            _pressurePlate = null;
        else if (_pressurePlate != null)
            _button = null;

        _startPos = transform.position;
    }

    void Update()
    {
        // If a button has been pressed, permanently move platform
        if (_button.GetButtonState())
            if (!_isMoving)
            {
                _isMoving = true;
                StartCoroutine(MovePlatform(_targetTransform.position, _platformMoveTime));
                _movedToTarget = !_movedToTarget;
            }
        
        /// Lös hur fan man ska hantera en pressure plate som antingen är on eller off
        /*
        // If a pressure plate is being pressed, move platform while true
        if (_pressurePlate.GetPlateState())
            if (!_isMoving)
            {
                _isMoving = true;
                StartCoroutine(MovePlatform(_targetTransform.position, _platformMoveTime));
                _movedToTarget = !_movedToTarget;
            }
        */

        // Debug test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_movedToTarget)
            {
                StartCoroutine(MovePlatform(_targetTransform.position, _platformMoveTime));
                _movedToTarget = !_movedToTarget;
            }

            else
            {
                StartCoroutine(MovePlatform(_targetTransform.position, _platformMoveTime));
                _movedToTarget = !_movedToTarget;
            }
        }
    }

    IEnumerator MovePlatform(Vector3 targetPos, float time)
    {
        float elapsedTime = 0;

        if (!_movedToTarget)
        {
            Debug.Log("Should move to target position");
            Vector3 _currentPos = transform.position;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(_currentPos, targetPos, (time / 3f));
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        else
        {
            Debug.Log("Should return to start position");
            Vector3 _currentPos = transform.position;

            while (elapsedTime < time)
            {
                transform.position = Vector3.Lerp(_currentPos, _startPos, time);
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
