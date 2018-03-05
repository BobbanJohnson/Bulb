using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDoor : PowerModule
{
    void Awake()
    {
        OnPowerStateChanged += (inNewPowerState) =>
        {
            if (inNewPowerState)
                OpenDoor();
        };
    }

    void OpenDoor()
    {
        Debug.Log("Bleep! OPen dooR");
    }
}
