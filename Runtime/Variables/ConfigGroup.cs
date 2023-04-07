using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "Config Group", menuName = VariableConst.MenuPath.Config_Group, order = VariableConst.Order.Config)]
    public class ConfigGroup : AssetT<List<Asset>>
    {
        public List<AssetGroup> groups;
        public AssetGroup defaultGroup;
        Dictionary<string, Asset> dictAsset = null;
        public void ReinitAsset()
        {
            for(int i = 0; i < groups.Count; i++)
            {
                if (groups[i].activeTarget == false) continue;

                setData(groups[i]);

                return;
            }
            setData(defaultGroup);

        }
        private void setData(AssetGroup _group)
        {
            if (dictAsset == null)
            {
                dictAsset = new Dictionary<string, Asset>();
                for (int i = 0; i < Value.Count; i++)
                {
                    var key = Value[i].getAssetVariableName();
                    if (!dictAsset.ContainsKey(key))
                    {
                        dictAsset.Add(key, Value[i]);
                    }
                }
            }

            //reinit config
            var group = _group.Assets;
            for (int k = 0; k < group.Count; k++)
            {
                var key = group[k].getAssetVariableName(true);
                Asset asset = null;
                if (!dictAsset.TryGetValue(key, out asset))
                {
#if UNITY_EDITOR
                    Debug.LogWarningFormat("[Editor] Asset with name {0} not found", key);
#endif
                    continue;
                }

                asset.FromOtherAsset(group[k]);
            }
        }
    }
    [Serializable]
    public class AssetGroup
    {
        public BoolAsset activeTarget;
        public List<Asset> Assets;
    }
}