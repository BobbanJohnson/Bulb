using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConnector : MonoBehaviour
{
    [SerializeField] List<PowerModule> _connectedModules;
    [SerializeField] bool _connectViaSelf;

    PowerModule _ownPowerModule;

    void Awake()
    {
        if (_connectViaSelf)
        {
            _ownPowerModule = GetComponent<PowerModule>();
            if (!_ownPowerModule)
                Debug.LogError("Connectors using 'Connect Via Self' requires a PowerModule component!");
        }
    }

    void OnTriggerEnter(Collider inCollider)
    {
        PowerModule inPowerModule = inCollider.GetComponent<PowerModule>();
        if (inPowerModule)
            foreach (PowerModule powerModule in _connectedModules)
            {
                if (_connectViaSelf)
                    powerModule.Connect(_ownPowerModule);
                else
                    powerModule.Connect(inPowerModule);

            }
    }

    void OnTriggerExit(Collider inCollider)
    {
        PowerModule inPowerModule = inCollider.GetComponent<PowerModule>();
        if (inPowerModule)
            foreach (PowerModule powerModule in _connectedModules)
            {
                if (_connectViaSelf)
                    powerModule.Disconnect(_ownPowerModule);
                else
                    powerModule.Disconnect(inPowerModule);
            }
    }
}
