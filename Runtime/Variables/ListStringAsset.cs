using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T70.VariableAsset;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "File Collection Asset", menuName = VariableConst.MenuPath.ListString_Collection, order = VariableConst.Order.File_Collection)]
public class ListStringAsset : ListAsset<string, StringAsset>
{
    public IntAsset TotalItems;

    public override List<string> Value
    {
        get { return base.Value; }
        set
        {
            base.Value = value;
            if(TotalItems != null) TotalItems.Value = value.Count;
        }
    }
    
    public override void FromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        var strs = value.Trim().Split(';');
        if (strs.Length <= 0) return;
        Value = new List<string>();
        for (int i = 0; i < strs.Length; i++)
        {
            Value.Add(strs[i].Trim());
        }
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Variable Asset/List String")]
    static void CreateAsset()
    {
        CreateSO<ListStringAsset>("list.string", "current.string.index", "current.string");
    }
#endif
}