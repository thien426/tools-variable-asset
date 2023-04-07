using System.Collections;
using System.Collections.Generic;
using T70.VariableAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ListIntAsset : ListAsset<int, IntAsset>
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Variable Asset/List Int")]
    static void CreateAsset()
    {
        CreateSO<ListIntAsset>("list.int", "current.index", "current.value");
    }
#endif
    public override void FromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        var strs = value.Split(';');
        if (strs.Length <= 0) return;
        Value = new List<int>();
        for (int i = 0; i < strs.Length; i++)
        {
            int.TryParse(strs[i], out int val);
            Value.Add(val);
        }
    }
}
