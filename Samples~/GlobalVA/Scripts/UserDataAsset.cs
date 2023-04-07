using UnityEngine;
using System;
using com.team70;
using T70.VariableAsset;
using UnityEngine.Events;

[Serializable] 
public class UserData
{
    [SerializeField] private int version = 0;
    public int Version => version;
    
    public void SetVersion(int ver)
    {
        version = ver;
    }
    
    [SerializeField] private bool music = true;
    public bool Music => music;

    public void EnableMusic(bool enable)
    {
        music = enable;
    }
    
    [SerializeField] private bool sound = true;
    public bool Sound => sound;
    
    public void EnableSound(bool enable)
    {
        sound = enable;
    }
}

[CreateAssetMenu(fileName = "User Data", menuName = VariableConst.MenuPath.User_Data,
    order = VariableConst.Order.User_Data)]
public class UserDataAsset : AssetT<UserData>
{
    string GetKey => string.Concat(getAssetVariableName(), "_data");
    
    public const int CurrentVersion = 0;

    public BoolAsset enableMusicAsset;
    public BoolAsset enableSoundAsset;

    public StringAsset buildVersionAsset;
    
    public override void RuntimeInit()
    {
        base.RuntimeInit();

        if (!HaveData)
        {
            CreateUserInfo();
        }
        else
        {
            CheckVersion();
        }

        enableMusicAsset.Value = Value.Music;
        enableMusicAsset.AddListener(ChangeMusic);
        PlayerPrefs.SetInt("enableMusic", Value.Music ? 1 : 0);

        enableSoundAsset.Value = Value.Sound;
        enableSoundAsset.AddListener(ChangeSound);
        PlayerPrefs.SetInt("enableSound", Value.Sound ? 1 : 0);
        
        Save();
    }
    
    #region SETTINGS
    private void ChangeMusic(bool enable)
    {
        Value.EnableMusic(enable);

        PlayerPrefs.SetInt("enableMusic", enable ? 1 : 0);
        
        Save();
    }

    private void ChangeSound(bool enable)
    {
        Value.EnableSound(enable);

        PlayerPrefs.SetInt("enableSound", enable ? 1 : 0);
        
        Save();
    }

    public void SetEnableMusic(bool enable)
    {
        enableMusicAsset.Value = enable;
    }

    public void SetEnableSound(bool enable)
    {
        enableSoundAsset.Value = enable;
    }
    #endregion

    private void CreateUserInfo()
    {
        Value = new UserData();
        
        Value.SetVersion(CurrentVersion);
        
        Save();
    }

    private void CheckVersion()
    {
        var version = Value.Version;
        if (CurrentVersion == version) return;
        
        if (version < 1)
        {
            CreateUserInfo();
        }
        
        Value.SetVersion(CurrentVersion);
        
        Save();
    }
    
    private bool HaveData => PlayerPrefs.HasKey(GetKey);
    
    [NonSerialized] private bool _isDirty = false;
    public void Save()
    {
        if (_isDirty) return;

        _isDirty = true;
        Async.Call(() =>
        {
            _isDirty = false;
            
            var key = string.Concat(getAssetVariableName(), "_data");
        
            PlayerPrefs.SetString(key, ToJson());
        });
    }

    [ContextMenu("Save")]
    public void SaveEditor()
    {
        PlayerPrefs.SetString(GetKey, ToJson());
    }
}

[Serializable]
public class IntEvent : UnityEvent<int>
{
}