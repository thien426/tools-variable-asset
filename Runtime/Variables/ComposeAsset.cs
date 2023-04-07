using System;
using System.IO;
using T70.VariableAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ComposeAsset<T> : AssetT<T>
{
    #if UNITY_EDITOR
    internal static ET CreateElement<ET>(Asset parent, string eName) where ET : Asset
    {
        var element = CreateInstance<ET>();
        element.name = eName;
        element.parentAsset = parent;
        AssetDatabase.AddObjectToAsset(element, parent);
        return element;
    }

    internal static MT CreateSO<MT>(string soName) where MT: Asset
    {
        var basePath = "Assets/";
        
        if (!soName.StartsWith(basePath, StringComparison.Ordinal))
        {
            if (Selection.activeObject != null)
            {
                string sPath = AssetDatabase.GetAssetPath(Selection.activeObject);
                basePath = Directory.Exists(sPath) ? sPath : sPath.Substring(0, sPath.LastIndexOf('/'));
            }
            
            soName = Path.Combine(basePath, soName + ".asset");
        }
        
        var main = CreateInstance<MT>();
        AssetDatabase.CreateAsset(main, soName);
        return main;
    }
    #endif
}
