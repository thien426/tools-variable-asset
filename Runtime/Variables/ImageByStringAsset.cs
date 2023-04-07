using System;
using System.Collections;
using System.Collections.Generic;
using T70.VariableAsset;
using UnityEngine;

[Serializable] 
public class ImageByStringData
{
    public string NameId;
    public Sprite image;
}
namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "Image By String Asset", menuName = VariableConst.MenuPath.ImageByString)]
    public class ImageByStringAsset : AssetT<ImageByStringData>
    {

    }
}