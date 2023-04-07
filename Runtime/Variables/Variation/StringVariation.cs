using System;
using UnityEngine;

namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "String Variation", menuName = "Variable Asset/Variation/String")]
    public class StringVariation : StringAsset
    {
        [Serializable] internal class Info : VariationInfo<string, StringAsset> {}
        
        [SerializeField] internal Info info;
        
        public override string Value
        {
            get { return info.Value; }
        }
    }
}