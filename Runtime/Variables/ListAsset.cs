
using System.Collections.Generic;
using UnityEngine;
using T70.VariableAsset;
using System;
using System.IO;
using System.Linq;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ListAsset <T, TAsset> : AssetT<List<T>>, IParentAsset where TAsset : AssetT<T>
{
    public IntAsset selectedIndex;
    public TAsset selectedAsset;
    
    public bool enableCache = false;
    [NonSerialized] private HashSet<T> _lstHashSet = new HashSet<T>();
    
    protected override void Init()
    {
        base.Init();

        Validate();
    }

    internal override void TriggerChange()
    {
        if (enableCache) _lstHashSet = new HashSet<T>(Value);
        
        base.TriggerChange();
    }
    
    public T this[int index]
    {
        get { return Value[index]; }
        set
        {
            var val = Value[index];
            Value[index] = value;
            
            if (enableCache == false) return;
            
            _lstHashSet.Remove(val);
            _lstHashSet.Add(value);
        }
    }
    
    public int Count { get { return Value.Count; }}

    public void Add(T t)
    {
        Value.Add(t);
        if (enableCache) _lstHashSet.Add(t);
    }
    
    public void Remove(T t)
    {
        Value.Remove(t);
        if (enableCache) _lstHashSet.Remove(t);
    }

    public void RemoveAt(int idx)
    {
        if (idx >= Value.Count) return;
        
        if (enableCache) _lstHashSet.Remove(Value[idx]);
        Value.RemoveAt(idx);
    }
    
    public bool Contains(T t)
    {
        return enableCache ? _lstHashSet.Contains(t) : Value.Contains(t);
    }
    
    public void Randomize1()
    {
        if (!_binded) Bind();
        
        int n = Value.Count;
        int v = Random.Range(0, n * 1000) % n;
        if (v == selectedIndex) v = (v + 1) % n;
        selectedIndex.Value = v;
    }

    [NonSerialized] protected List<int> randomMap;
    [NonSerialized] public bool randomListDirty = true;
    public virtual void Randomize()
    {
        if (!_binded) Bind();

        int n = Count;
        CheckGenerateRandomMap(n);

        var idx = randomMap.IndexOf(selectedIndex.Value);
        idx = (idx + 1) % n; // next value

        //Debug.Log("Random --> " + idx + " --> " + randomMap[idx]);

        var v = randomMap[idx];
        if (v == selectedIndex) v = (v + 1) % n;
        selectedIndex.Value = v;
    }

    protected void Shuffle(List<int> list)
    {
        var n = list.Count;
        for (var i = 0; i < n - 1; i++)
        {
            var j = (Random.Range(0, 1000 * n) % (n - i)) + i;

            // swap value for i & j
            var tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }

    void CheckGenerateRandomMap(int n)
    {
        if (randomMap == null) randomMap = new List<int>();
        if (randomMap.Count != n || randomListDirty)
        {
            randomListDirty = false;
            randomMap.Clear();

            for (var i = 0; i < n; i++)
            {
                randomMap.Add(i);
            }

            // randomize
            Shuffle(randomMap);
            Debug.Log("Generated: " + String.Join(",", randomMap.Select(item => item.ToString()).ToArray()));//
        }
    }
    
    public void Validate() // call manually after change Value []
    {
        #if UNITY_EDITOR
        EditorApplication.update -= Validate;
        #endif
        
        if (selectedIndex == null || selectedAsset == null) return;
        if (Value == null) return;
        
        var max = Value.Count;
        if (max == 0) return; // no data

        selectedIndex.min = 0;
        selectedIndex.max = max-1;
        
        if (selectedIndex.Value >= max)
        {
            selectedIndex.Value = max-1;
            selectedAsset.Value = Value[selectedIndex];
        }
    }
    

    #if UNITY_EDITOR
    internal static void CreateSO<TList>(string collectionName, string currentIndexName="index", string currentAssetName="asset") where TList: ListAsset<T, TAsset>
        {
            var basePath = "Assets/";
            
            if (!collectionName.StartsWith(basePath, StringComparison.Ordinal))
            {
                if (Selection.activeObject != null)
                {
                    string sPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                    basePath = Directory.Exists(sPath) ? sPath : sPath.Substring(0, sPath.LastIndexOf('/'));
                }
                
                collectionName = Path.Combine(basePath, collectionName + ".asset");
            }
            
            var main = CreateInstance<TList>();
            AssetDatabase.CreateAsset(main, collectionName);
    
            var index = CreateInstance<IntAsset>();
            var asset = CreateInstance<TAsset>();
            
            index.name = currentIndexName;
            asset.name = currentAssetName;
            
            AssetDatabase.AddObjectToAsset(index, main);
            AssetDatabase.AddObjectToAsset(asset, main);
    
            main.selectedIndex = index;
            main.selectedAsset = asset;
            index.parentAsset = main;
            asset.parentAsset = main;
            
            AssetDatabase.SaveAssets();
    }
    
    void OnValidate()
    {
        if (Application.isPlaying) return;

        //EditorApplication.update -= Validate;
        //EditorApplication.update += Validate;
    }
    #endif
    
    
    public void ConnectChildren()
    {
        if (!_binded) Bind();
    }
    
    [NonSerialized] protected bool _binded; // why the stupid Unity serialize private property here?

    protected void Bind()
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
        Validate();
        selectedAsset.Value = Value[index];
    }
}