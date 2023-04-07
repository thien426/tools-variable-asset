using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "String Variation", menuName = "Variable Asset/Variation/List String")]
    public class ListStringVariation : ListStringAsset
    {
        [Serializable] internal class Info : VariationInfo<List<string>, ListStringAsset> { }

        [SerializeField] internal Info info;

        public override List<string> Value
        {
            get { return info.Value; }
        }
    }
}
