using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "Config Asset", menuName = VariableConst.MenuPath.Config, order = VariableConst.Order.Config)]
    public class ConfigAsset : AssetT<List<Asset>>
    {
        private Dictionary<string, Asset> _dictConfig;
        public Dictionary<string, Asset> DictConfig
        {
            get
            {
                if (_dictConfig != null) return _dictConfig;
                _dictConfig = Value.ToDictionary(x => getAssetVariableName(x), x => x);
                return _dictConfig;
            }
        }

        private string getAssetVariableName(Asset asset)
        {
            if (asset == null)
            {
                Debug.LogError("Asset null");
                return string.Empty;
            }
            return asset.getAssetVariableName();
        }
        
        [ContextMenu("Debug Name")]
        private void DebugAllName()
        {
            foreach(var item in Value)
            {
                Debug.Log(getAssetVariableName(item));
            }
        }

        public void FromDictionary(Dictionary<string, string> data)
        {
            Dictionary<string, Asset> dictAsset = new Dictionary<string, Asset>();
            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i] == null)
                {
                    Debug.LogError("Asset is null in index: " + i);
                    continue;
                }
                
                var key = getAssetVariableName(Value[i]);
                if (!dictAsset.ContainsKey(key))
                {
                    dictAsset.Add(key, Value[i]);
                }
            }
            
            foreach (var item in data)
            {
                Asset asset = null;
                if (!dictAsset.TryGetValue(item.Key, out asset))
                {
#if UNITY_EDITOR
                    Debug.LogWarningFormat("[Editor] Asset with name {0} not found", item.Key);
#endif
                    continue;
                }
                asset.FromString(item.Value);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("PolishAssets")]
        public void PolishAssets()
        {
            HashSet<Asset> hash = new HashSet<Asset>();

            List<Asset> list = this.Value;
            
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Asset item = list[i];
                if (item == null || hash.Contains(item))
                {
                    list.RemoveAt(i);
                    continue;
                }
                
                hash.Add(item);
            }
            
            list.Sort((item1, item2) => String.Compare(item1.name, item2.name, StringComparison.Ordinal));
            
            EditorUtility.SetDirty(this);
        }
        #endif
    }
}