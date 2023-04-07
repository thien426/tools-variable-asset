using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using T70.VariableAsset;
public interface IVAssetConfig
{
    void SetAsset(Asset asset);
}
public class VAssetConfig<T> : MonoBehaviour, IVAssetConfig where T : T70.VariableAsset.Asset
{
    public T asset;
    public Text labelTf;
    


    #if UNITY_EDITOR
    internal void OnValidate()
    {
        if (Application.isPlaying) return;
        if (asset == null) return;

        UnityEditor.EditorApplication.update -= Validate;
        UnityEditor.EditorApplication.update += Validate;
    }
#endif
    internal virtual void Validate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update -= Validate;
#endif
        if (labelTf != null) labelTf.text = asset.name;
    }
   
    

    public virtual void SetAsset(Asset asset)
    {
        this.asset = (T)asset;
        Validate();
    }
}
