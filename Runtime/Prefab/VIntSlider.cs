using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using T70.VariableAsset;

public class VIntSlider : VAssetConfig<IntAsset>
{
    public Slider slider;
    public Text valueTf;

    [ContextMenu("Validate")]
    override internal void Validate()
    {
        base.Validate();
       
        slider.minValue = asset.min;
        slider.maxValue = asset.max;
        slider.value = asset.Value;
        valueTf.text = slider.value.ToString();
        Init();
    }
    
    float revertValue;

    private void OnEnable()
    {
        Init();
    }
    
    private void Init()
    {
        if (asset == null) return;
        revertValue = asset.Value;
        slider.value = asset;
        slider.onValueChanged.RemoveListener(OnSlider);
        slider.onValueChanged.AddListener(OnSlider);
        valueTf.text = slider.value.ToString();
    }

    private void OnDisable() 
    {
        slider.onValueChanged.RemoveListener(OnSlider);
    }

    void OnSlider(float value)
    {
        if (slider == null) return;
        
        var vv = Mathf.RoundToInt(slider.value);
        if (Mathf.Abs(slider.value - vv) < 0.01f) return;

        slider.value = vv;

        if (valueTf != null) valueTf.text = vv.ToString();
        if (asset != null) asset.Value = vv;
    }

    public void OnClickRevert()
    {
        slider.value = revertValue;
    }
}
