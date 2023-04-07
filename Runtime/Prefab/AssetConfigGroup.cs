using System.Collections.Generic;
using UnityEngine;
using System;
using T70.VariableAsset;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Serializable]
public class VariableGroup
{
    public string groupName;
    
    // BugFix NullReferenceException: Object reference not set to an instance of an object
    // https://developer.cloud.unity3d.com/diagnostics/orgs/team70/projects/d62d5a15-9012-418e-a408-140af1e3276e/crashes/29e8bbccc42f5c52f45d0ede093036b2/
    public List<Asset> lstAsset = new List<Asset>();

    private List<GameObject> _configInited = new List<GameObject>();
    private bool _showed = true;
    private Text _txtGroupName; //runtime

    private Dictionary<string, GameObject> _cacheResourceLoad;

    public void Init(Transform parent, Button btnLabelPrefab)
    {
        // BugFix NullReferenceException: Object reference not set to an instance of an object
        // https://developer.cloud.unity3d.com/diagnostics/orgs/team70/projects/d62d5a15-9012-418e-a408-140af1e3276e/crashes/29e8bbccc42f5c52f45d0ede093036b2/
        if (btnLabelPrefab == null) return;
        
        var btnLabel = Object.Instantiate(btnLabelPrefab, parent);
        
        // BugFix NullReferenceException: Object reference not set to an instance of an object
        // https://developer.cloud.unity3d.com/diagnostics/orgs/team70/projects/d62d5a15-9012-418e-a408-140af1e3276e/crashes/29e8bbccc42f5c52f45d0ede093036b2/
        if (btnLabel == null) return;
        
        _txtGroupName = btnLabel.GetComponentInChildren<Text>();
        
        // BugFix NullReferenceException: Object reference not set to an instance of an object
        // https://developer.cloud.unity3d.com/diagnostics/orgs/team70/projects/d62d5a15-9012-418e-a408-140af1e3276e/crashes/29e8bbccc42f5c52f45d0ede093036b2/
        if (_txtGroupName == null) return;
        
        _txtGroupName.text = groupName;
        btnLabel.onClick.RemoveAllListeners();
        btnLabel.onClick.AddListener(ToggleShow);

        _configInited = new List<GameObject>();
        _cacheResourceLoad = new Dictionary<string, GameObject>();

        foreach (var item in lstAsset)
        {
            if (item == null) continue;
            if (string.IsNullOrEmpty(item.EditorAssetId)) continue;
            
            GameObject prefab = null;
            if (!_cacheResourceLoad.TryGetValue(item.EditorAssetId, out prefab))
            {
                prefab = Resources.Load<GameObject>(item.EditorAssetId);
                if (prefab == null) continue;
                _cacheResourceLoad.Add(item.EditorAssetId, prefab);
            }
            
            var configView = Object.Instantiate(prefab, parent);
            _configInited.Add(configView);
            
            var vAsset = configView.GetComponent< IVAssetConfig>();
            if (vAsset == null)
            {
                Debug.LogWarning("Cannot cast target to VAssetConfig " + configView.name);
                continue;
            }
            vAsset.SetAsset(item);
        }

        SetShow(true);
    }

    private void SetShow(bool isShow)
    {
        _showed = isShow;
        foreach(var item in _configInited)
        {
            item.SetActive(isShow);
        }
    }

    private void ToggleShow()
    {
        SetShow(!_showed);
    }
}
public class AssetConfigGroup : MonoBehaviour
{
    public Button btnGroupPrefab;
    public List<VariableGroup> groups;

    private void Start()
    {
        for(int i = 0; i< groups.Count; i++)
        {
            groups[i].Init(transform, btnGroupPrefab);
        }
    }
}
