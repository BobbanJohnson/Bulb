using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConnector : MonoBehaviour
{
    [SerializeField] List<PowerModule> _connectedModules;


    void OnTriggerEnter(Collider inCollider)
    {
        PowerModule inPowerModule = inCollider.GetComponent<PowerModule>();
        if (inPowerModule)
            foreach (PowerModule powerModule in _connectedModules)
                powerModule.Connect(inPowerModule);
    }

    void OnTriggerExit(Collider inCollider)
    {
        PowerModule inPowerModule = inCollider.GetComponent<PowerModule>();
        if (inPowerModule)
            foreach (PowerModule powerModule in _connectedModules)
                powerModule.Disconnect(inPowerModule);
    }
}
