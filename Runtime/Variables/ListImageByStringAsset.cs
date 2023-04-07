using System.Collections;
using System.Collections.Generic;
using T70.VariableAsset;
using UnityEngine;
[CreateAssetMenu(fileName = "List Song Cards", menuName = VariableConst.MenuPath.ListImageByString)]
public class ListImageByStringAsset : AssetT<List<ImageByStringData>>
{
    
    public ImageByStringData this[int index]
    {
        get
        {
            if (Value == null || Value.Count == 0) return null;
            if (index < 0 || index > Value.Count - 1) return null;

            return Value[index];
        }
        set
        {
            Value[index] = value;
        }
    }

    public Sprite GetSpriteByNameID(string nameId)
    {
        if(string.IsNullOrEmpty(nameId)) return null;

        foreach(ImageByStringData data in Value)
        {
            if (data.NameId.Equals(nameId)) return data.image;
        }

        return null;
    }

}
