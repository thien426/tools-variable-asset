using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using T70.VariableAsset;


[CreateAssetMenu(fileName = "Float collection Asset", menuName = VariableConst.MenuPath.Float_Collection, order = VariableConst.Order.Float_Collection)]
public class FloatCollectionAsset : CollectionAsset<float, FloatAsset>
{
    [ContextMenu("Bind")]
    override public void Bind()
    {
        base.Bind();
    }
    public override void FromString(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        var strs = value.Split(';');
        if (strs.Length <= 0) return;
        Value = new List<float>();
        for (int i= 0; i < strs.Length; i++)
        {
            float.TryParse(strs[i], NumberStyles.Float, CultureInfo.InvariantCulture, out float val);
            Value.Add(val);
        }
    }
}
