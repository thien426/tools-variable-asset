using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using T70.VariableAsset;

public class VSlider : VAssetConfig<FloatAsset>
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

    float accuracy;

    private void Start()
    {
        slider.minValue = asset.min;
        slider.maxValue = asset.max;
        slider.value = asset.Value;
        valueTf.text = slider.value.ToString();
        Init(); //DO NOT delete
    }

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

        if (accuracy == 0)
        {
            var mag = Mathf.Log10(slider.maxValue - slider.minValue); // order of magnitude of the range we are inspecting
            accuracy = Mathf.Pow(10, Mathf.CeilToInt(2-mag)); // accuracy : 1/1000 of the value being inspected
        }
    }

    private void OnDisable() 
    {
        slider.onValueChanged.RemoveListener(OnSlider);
    }

    void OnSlider(float value)
    {
        if (slider == null) return;

        //var v1 = value - slider.minValue;
        var vv = Mathf.RoundToInt(accuracy * value) / accuracy;// + slider.minValue;

        //Debug.Log(vv + ": accuracy : " + accuracy);
        
        if (valueTf != null) valueTf.text = vv.ToString();
        if (asset != null) asset.Value = vv;
    }

    public void OnClickRevert()
    {
        slider.value = revertValue;
    }
}
