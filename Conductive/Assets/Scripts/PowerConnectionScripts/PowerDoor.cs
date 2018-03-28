using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDoor : PowerModule
{
    CameraBehaviour _mainCamera;
    [SerializeField] bool _shouldPan;

    void Awake()
    {
        _mainCamera = FindObjectOfType<CameraBehaviour>();

        OnPowerStateChanged += (inNewPowerState) =>
        {
            if (inNewPowerState)
            {
                OpenDoor();
                if (_shouldPan)
                    _mainCamera.CameraPan();
            }
                
        };
    }

    void OpenDoor()
    {
        Debug.Log("Bleep! OPen dooR");
    }
}
