using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Smooth camera variables")]
    [SerializeField] float _smoothSpeed;
    [SerializeField] Transform _lookAt;
    [SerializeField] Vector3 _offset;

    [Header("Camera pan variables")]
    [SerializeField] Transform _exitTransform;
    [SerializeField] float _cameraPanTime;
    [SerializeField] float _secondsToWaitFor;

    // Player management variables
    PlayerManager _playerManager;
    PlayerMovement _currentPlayer;


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
        {
            PanCamera(_exitTransform.position);
        }
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


    public void PanCamera(Vector3 inTargetPos)
    {
        StartCoroutine(panCameraToPosition(inTargetPos, _cameraPanTime));
    }


    IEnumerator panCameraToPosition(Vector3 targetPos, float time)time
    {
        // Get some positions to lerp to and from
        Vector3 currentPos = transform.position;
        targetPos += new Vector3(_offset.x, _offset.y, _offset.z);

        _currentPlayer.ToggleMovement(false);

        // Lerp from current position to target position
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            if (elapsedTime == time)
            {
                yield return new WaitForSeconds(3);
                break;
            }

            else
            {
                transform.position = Vector3.Lerp(currentPos, targetPos, (elapsedTime / time));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        _currentPlayer.ToggleMovement(true);
    }
}
