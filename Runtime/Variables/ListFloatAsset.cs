using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using T70.VariableAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[CreateAssetMenu(fileName = "List Float", menuName = "Variable Asset/List Float", order = VariableConst.Order.Theme_Collection)]
public class ListFloatAsset : ListAsset<float, FloatAsset>
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Variable Asset/List Float")]
    static void CreateAsset()
    {
        CreateSO<ListFloatAsset>("list.float", "current.index", "current.value");
    }
#endif
    public override void FromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        var strs = value.Split(';');
        if (strs.Length <= 0) return;
        Value = new List<float>();
        for (int i = 0; i < strs.Length; i++)
        {
            float.TryParse(strs[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float val);
            Value.Add(val);
        }
    }
}
