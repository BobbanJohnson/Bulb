using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbToggle : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material _unpoweredMaterial;
    [SerializeField] Material _poweredMaterial;

    void Awake()
    {
        // Get powermodule component, Add function to event, // Anon func param,  // _meshRenderer.mat = _poweredMaterial if the param is true, else _unpoweredMaterial
        GetComponent<PowerModule>().OnPowerStateChanged += inNewPowerState => _meshRenderer.material = inNewPowerState ? _poweredMaterial : _unpoweredMaterial;
    }
}