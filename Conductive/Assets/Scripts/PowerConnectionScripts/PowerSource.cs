using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : PowerModule
{
    [SerializeField] bool _isGeneratingPower = true;
    public bool isGeneratingPower
    {
        get
        {
            return _isGeneratingPower;
        }

        set
        {
            _isGeneratingPower = value;

            if (_isGeneratingPower)
                isPowered = true;

            UpdateConnection();
        }
    }

    void Start()
    {
        isGeneratingPower = _isGeneratingPower;
    }


    public void Toggle()
    {
        isGeneratingPower = !isGeneratingPower;
    }
}