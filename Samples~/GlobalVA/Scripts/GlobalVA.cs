using System.Collections.Generic;
using UnityEngine;
using System.Text;
using T70.VariableAsset;

[CreateAssetMenu(fileName = "GlobalVA", menuName = "Assets/Global VA")]
public class GlobalVA : ScriptableObject
{
    private static GlobalVA instance;
    
    public void Init()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instance found for GlobalVA");
            return; // ignore !
        }
        
        instance = this;
        
        RuntimeInit();
    }

    public void RuntimeInit()
    {
        _userDataAsset.RuntimeInit();
    }
    
    public static bool IsInit => instance != null;

    [SerializeField] internal UserDataAsset _userDataAsset;
    public static UserDataAsset userDataAsset
    {
        get { if(instance != null) return instance._userDataAsset; return null; }
    }
}
