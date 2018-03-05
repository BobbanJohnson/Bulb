using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerModule : MonoBehaviour
{
    [SerializeField] List<PowerModule> _modulesToConnect = new List<PowerModule>();

    protected bool _isPowered;
    protected bool isPowered
    {
        get
        {
            return _isPowered;
        }

        set
        {
            if (value != _isPowered)
                if (OnPowerStateChanged != null)
                    OnPowerStateChanged(value);

            _isPowered = value;

            if (value)
                GetComponent<MeshRenderer>().material.color = Color.yellow;
            else
                GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    List<PowerModule> _connectedModules = new List<PowerModule>();

    public event Action<bool> OnPowerStateChanged;


    void Awake()
    {
        isPowered = false;
        foreach (PowerModule powerModule in _modulesToConnect)
            powerModule.Connect(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(_modulesToConnect.Count);
            foreach (PowerModule powerModule in _modulesToConnect)
                powerModule.Connect(this);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (this is PowerSource)
            {
                PowerSource powerSource = (PowerSource)this;
                if (powerSource)
                    powerSource.isGeneratingPower = !powerSource.isGeneratingPower;
            }
        }
    }


    public void Connect(PowerModule inNewConnection)
    {
        if (_connectedModules.Contains(inNewConnection))
            return;

        _connectedModules.Add(inNewConnection);

        inNewConnection.Connect(this);

        UpdateConnection();
    }

    public void Disconnect(PowerModule inLostConnection)
    {
        if (!_connectedModules.Contains(inLostConnection))
            return;

        _connectedModules.Remove(inLostConnection);

        inLostConnection.Disconnect(this);

        UpdateConnection();
    }


    protected void UpdateConnection()
    {
        bool circuitIsPowered = false;

        // Check if this node is connected to a powersource
        List<PowerModule> visited = GetConnectedNodes(new List<PowerModule>());
        foreach (PowerModule powerModule in visited)
            if (powerModule is PowerSource)
                if (((PowerSource)powerModule).isGeneratingPower)
                    circuitIsPowered = true;

        // Set the power state of all connected nodes to the result
        foreach (PowerModule powerModule in visited)
            powerModule.isPowered = circuitIsPowered;
        isPowered = circuitIsPowered;
    }

    List<PowerModule> GetConnectedNodes(List<PowerModule> inVisited)
    {
        inVisited.Add(this);

        // Find all unvisited neighbours
        foreach (PowerModule powerModule in _connectedModules)
            if (!inVisited.Contains(powerModule))
                powerModule.GetConnectedNodes(inVisited);

        return inVisited;
    }
}