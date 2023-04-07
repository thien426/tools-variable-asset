using System;
using System.Collections.Generic;
using UnityEngine;

namespace T70.VariableAsset
{
    public interface IVariationIndex
    {
        int GetVariationIndex();
    }
    
    [Serializable] public class VariationInfo<TData, TAsset> where TAsset : AssetT<TData>
    {
        public Asset control;
        public List<TAsset> listOptions;
        [NonSerialized] private IVariationIndex controlVI;

        public TData Value
        {
            get
            {
                if (controlVI == null) controlVI = control as IVariationIndex;
                if (controlVI == null)
                {
                    Debug.LogWarning("ControlVI should not be null: " + control);
                    return default(TData);
                }
                
                var index = controlVI.GetVariationIndex();
                return listOptions[index].Value;    
            }
        }
    }
}
