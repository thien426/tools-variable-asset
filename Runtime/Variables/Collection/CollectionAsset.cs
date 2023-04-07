using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T70.VariableAsset;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

public class CollectionAsset<T, TAsset> : AssetT<List<T>> where TAsset : AssetT<T>
{
    public T this[int index]
    {
        get { return Value[index]; }
        set { Value[index] = value; }
    }
    public int Count { get { return Value.Count; }}
    
    // ------------- BINDING -----------------
    public IntAsset selectedIndex;
    public TAsset selectedAsset;

    public override void FromJson(string json)
    {
        IntAsset cache = selectedIndex;
        TAsset cacheT = selectedAsset;
        base.FromJson(json);
        this.selectedIndex = cache;
        this.selectedAsset = cacheT;
        Bind();
    }
    
    [NonSerialized] private bool _binded; // why the stupid Unity serialize private property here?

    public virtual void Bind()
    {
        //Debug.Log("Try 2 Bind:: " + _binded + ":" + (selectedAsset == null) + ":" + (selectedIndex == null) + ":" + (Value == null) + ":" + (Value.Count == 0));

        if (_binded) return;
        if (selectedAsset == null) return;
        if (selectedIndex == null) return;
        if (Value == null) return;
        if (Value.Count == 0) return;
        
        _binded = true;
        
        selectedIndex.Value = 0;
        selectedAsset.Value = Value[selectedIndex];
        selectedIndex.AddListener(OnChangeIndex);
    }

    void OnChangeIndex(int index)
    {
        if (Value.Count <= index || index < 0)
        {
            Debug.LogWarning("Invalid Index " + index);
            return;
        }

        selectedAsset.Value = Value[index];
    }

    #if UNITY_EDITOR

    private void OnValidate() 
    {
        if (Application.isPlaying) return;
        
        //Debug.Log("OnValidate()!!!" + _binded + ":" + selectedIndex + ":" + selectedAsset + ":" + Value);
        #if UNITY_EDITOR
        if (selectedIndex != null)
        {
            selectedIndex.min = 0;
            selectedIndex.max = Value.Count-1;
        }
        #endif
        
        if (!_binded) Bind();
    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable()!!!" + selectedIndex + ":" + selectedAsset + ":" + Value);

        if (!_binded) Bind();
    }
    #endif
}
