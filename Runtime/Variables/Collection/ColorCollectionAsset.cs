using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using T70.VariableAsset;


[CreateAssetMenu(fileName = "Color collection Asset", menuName = VariableConst.MenuPath.Color_Collection, order = VariableConst.Order.Color_Collection)]
public class ColorCollectionAsset : CollectionAsset<Color, ColorAsset>
{
    [ContextMenu("Bind")]
    override public void Bind()
    {
        base.Bind();
    }
}
