using System;
using T70.VariableAsset;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColorCA : ComposeAsset<Color>, IParentAsset
{
    public FloatAsset r;
    public FloatAsset g;
    public FloatAsset b;
    public FloatAsset a;

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Variable Asset/Color CA")]
    static void CreateListColorAsset()
    {
        var main = CreateSO<ColorCA>("color.ca");
        main.r = CreateElement<FloatAsset>(main, "r");
        main.g = CreateElement<FloatAsset>(main, "g");
        main.b = CreateElement<FloatAsset>(main, "b");
        main.a = CreateElement<FloatAsset>(main, "a");
        AssetDatabase.SaveAssets();
    }
    #endif

    public override Color Value
    {
        get { return base.Value; }
        set
        {
            base.Value = value;

            // set value to elements, if changed
            if (r.Value != _value.r) r.Value = _value.r;
            if (g.Value != _value.g) g.Value = _value.g;
            if (b.Value != _value.b) b.Value = _value.b;
            if (a.Value != _value.a) a.Value = _value.a;
        }
    }
    
    [NonSerialized] private bool connected;
    public void ConnectChildren()
    {
        if (connected) return;
        
        connected = true;
        
        r.Value = Value.r;
        g.Value = Value.g;
        b.Value = Value.b;
        a.Value = Value.a;
        
        r.AddListener(OnElementChange);  
        g.AddListener(OnElementChange);  
        b.AddListener(OnElementChange);  
        a.AddListener(OnElementChange);  
        
        //Debug.LogWarning("Binded!");
    }
    
    private void OnElementChange(float v)
    {
        Value = new Color(r, g, b, a);
    }
}
