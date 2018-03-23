using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Smooth camera variables")]
    [SerializeField] float _smoothTime;
    [SerializeField] Transform _lookAt;
    [SerializeField] Vector3 _offset;
    [SerializeField] bool _followPlayer = true;

    [Header("Camera pan variables")]
    [SerializeField] Transform _exitTransform;
    [SerializeField] float _cameraPanTime;
    [SerializeField] float _secondsToWaitFor;

    // Player management variables
    Vector3 targetPosition { get { return _lookAt.transform.position + _offset; } }
    PlayerManager _playerManager;
    PlayerMovement _currentPlayer;
    Vector3 _velocity;


    void Awake()
    {
        _playerManager = FindObjectOfType<PlayerManager>();

        // Assign a new transform to look at when switching characters
        _playerManager.OnTargetPlayerChanged += SetFollowTarget;

        _currentPlayer = _playerManager.GetCurrentPlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            StartCoroutine(_PanCamera(_exitTransform.position, 2.0f));
    }


    void LateUpdate()
    {
        if (_followPlayer)
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }


    void SetFollowTarget(PlayerMovement inPlayer)
    {
        _lookAt = inPlayer.transform;
    }

    IEnumerator _PanCamera(Vector3 inTargetPosition, float inPanSpeed)
    {
        Transform originalLookAt = _lookAt;
        Vector3 startPos = originalLookAt.transform.position;

        GameObject lookAtObject = new GameObject();
        lookAtObject.transform.position = originalLookAt.transform.position;
        _lookAt = lookAtObject.transform;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * inPanSpeed;

            _lookAt.transform.position = Vector3.Lerp(startPos, inTargetPosition, timer);
            yield return null;
        }

        yield return new WaitForSeconds(_secondsToWaitFor);

        _lookAt = originalLookAt;
        Destroy(lookAtObject);
    }
}
