using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using T70.VariableAsset;

public class VToggle : VAssetConfig<BoolAsset>
{   
    public Toggle toggle;
    
    [ContextMenu("Validate")]
    override internal void Validate()
    {
        base.Validate();
        toggle.isOn = asset.Value;
        Init();
    }

    private void OnEnable()
    {
        Init();
    }
    
    private void Init()
    {
        if (asset == null) return;
        revertValue = asset.Value;

        toggle.isOn = asset.Value;
        toggle.onValueChanged.RemoveListener(OnToggle);
        toggle.onValueChanged.AddListener(OnToggle);
    }
    
    bool revertValue;
    
    private void OnDisable() 
    {
        toggle.onValueChanged.RemoveListener(OnToggle);
    }

    void OnToggle(bool value)
    {
        //Debug.Log("OnToggle: " + value);
        if (asset != null) asset.Value = toggle.isOn;
    }
}
