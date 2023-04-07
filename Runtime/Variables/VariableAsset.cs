
#if UNITY_EDITOR
	using UnityEditor;
#endif

using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace T70.VariableAsset
{
	// ----------------- ASSET --------------------
    public static class VariableConst
    {
        public static class MenuPath
        {
            #region System Variables
            public const string BOOL_ASSET = "Variable Asset/Boolean";
            public const string Int = "Variable Asset/Int";
            public const string Float = "Variable Asset/Float";
            public const string Float_Collection = "Variable Asset/Float Collection";
            public const string String = "Variable Asset/String";
            #endregion

            #region Unity Variable

            public const string AudioClip = "Variable Asset/Audio Clip";

            public const string Event = "Variable Asset/Event";
            public const string Vector2 = "Variable Asset/Vector2";
            public const string Vector3 = "Variable Asset/Vector3";

            public const string ImageByString = "Variable Asset/Image By String";
            public const string Color = "Variable Asset/Color";
            public const string Color_Collection = "Variable Asset/Color Collection";
            public const string Color2_Collection = "Variable Asset/Color Collection";
            public const string ListImageByString = "Variable Asset/Game/List Image By String";
            #endregion

            #region Special Game Variables
            public const string MusicImpact = "Variable Asset/AbTest/MusicImpact";
            public const string AbTest_Normal = "Variable Asset/AbTest/Normal";
            public const string AbTest_Collection = "Variable Asset/AbTest/AbTest Collection";
            public const string AbTest_EnableOption = "Variable Asset/AbTest/AbTest Enable Option";
            public const string AB_IntAsset = "Variable Asset/AbTest/AB IntAsset";
            public const string Background_Asset = "Variable Asset/Game/Background Asset";
            public const string EmbedCollection = "Variable Asset/Game/Embed Collection";
            public const string GameState = "Variable Asset/Game/Game State";
            public const string Config = "Variable Asset/Game/Config";
            public const string Config_Group = "Variable Asset/Game/Config Group";
            public const string ListString_Collection = "Variable Asset/Game/List String";
            public const string ListFormation = "Variable Asset/Game/List Formation";
	        #endregion

	        #region Collection
            public const string SpriteCollection = "Variable Asset/Collection/Sprite";
            public const string ColorCollection = "Variable Asset/Collection/Color";
            public const string SoundCollection = "Variable Asset/Collection/Sound";
            public const string StringCollection = "Variable Asset/Collection/String";
            public const string TextureCollection = "Variable Asset/Collection/Texture";
            public const string PrefabCollection = "Variable Asset/Collection/Prefab";
            #endregion
            
            #region Data
            public const string Data = "Variable Asset/Data/Data";
            public const string User_Data = "Variable Asset/Data/User Data";
            public const string User_Profile = "Variable Asset/Data/User Profile";
            public const string User_Analytic = "Variable Asset/Data/User Analytic";
            #endregion
            
            #region Battle
            public const string CharacterInfo = "Variable Asset/UI/Character Info";
            public const string TeamType = "Variable Asset/UI/Team Type";
            public const string PassBarConfig = "Variable Asset/UI/Pass Bar Config";
            public const string BattleArena= "Variable Asset/UI/Battle Arena";
            public const string TeamInfo= "Variable Asset/UI/Team Info";
            public const string ListRewardMatch= "Variable Asset/UI/List Reward Match";
            public const string TurnStatus = "Variable Asset/UI/Turn Status";
            public const string FinishActionType = "Variable Asset/UI/Finish Action Type";
            public const string FoulsAction = "Variable Asset/UI/Fouls Action";
            public const string ListRoomData = "Variable Asset/UI/List Room Data";

            #endregion
            
            #region Ads
            
            public const string MaxAdsData = "Variable Asset/Ads/Max Ads Data";
            
            #endregion
            
            #region Cheat
            
            public const string ActionSuccessType = "Variable Asset/Cheat/Action Success Type";
            public const string ActionFailType = "Variable Asset/Cheat/Action Fail Type";
            
            #endregion
        }
        public static class Order
        {
            #region System Variables
            public const int BOOL_ASSET = 1;
            public const int Int = 2;
            public const int Float = 3;
            public const int Float_Collection = 4;
            public const int String = 5;

            #endregion

            #region Unity Variable
            public const int Event = 100;
            public const int Vector2 = 101;
            public const int Vector3 = 102;

            public const int Color = 103;
            public const int Color_Collection = 104;
            public const int Color2_Collection = 105;
            public const int AudioClip = 106;
            #endregion

            #region Special Game Variables
            public const int MusicImpact = 500;
            public const int AbTest_Normal = 501;
            public const int AbTest_Collection = 502;
            public const int AB_IntAsset = 503;
            public const int Background_Asset = 504;
            public const int EmbedCollection = 505;
            public const int GameState = 506;
            public const int Config = 507;
            public const int File_Collection = 508;
            public const int ListFormation = 509;
            #endregion

            #region Collection
            public const int SpriteCollection = 800;
            public const int ColorCollection = 801;
            public const int SoundCollection = 802;
            public const int StringCollection = 803;
            public const int TextureCollection = 804;
            public const int PrefabCollection = 805;
            #endregion
            
            #region User
            public const int Data = 900;
            public const int User_Data = 901;
            public const int User_Profile = 902;
            public const int User_Analytic = 903;
            #endregion
            
            #region Battle
            public const int CharacterInfo = 701;
            public const int TeamType = 702;
            public const int PassBarConfig = 703;
            public const int BattleArena = 704;
            public const int TeamInfo = 705;
            public const int ListRewardMatch = 706;
            public const int TurnStatus = 707;
            public const int FinishActionType = 708;
            public const int FoulsAction = 709;
            public const int ListRoomData = 710;

            #endregion
            
            #region Ads
            
            public const int MaxAdsData = 200;
            
            #endregion

            #region Cheat
            
            public const int ActionSuccessType = 600;
            public const int ActionFailType = 601;
            
            #endregion
        }
    }


	public enum AssetMode
	{
		Persistent,		// Persistent changes when game play
		Runtime,		// Shared data between views - non persistent runtime changes
	}

	public interface IParentAsset
	{
		void ConnectChildren();
	}
	
	public class Asset : ScriptableObject 
	{
        public virtual string CacheName
        {
            get { return name.Replace('.', '_'); }
        }
        
        public string suffixName;
        [SerializeField] internal AssetMode mode;
        public Asset parentAsset;
        
        public virtual void FromJson(string json)
        {
            Debug.LogWarning("Child class of Asset should override FromJson()");
//             try
//             {
//                     JsonUtility.FromJsonOverwrite(json, this);
//                 TriggerChange();
// #if UNITY_EDITOR
//                 EditorUtility.SetDirty(this);
// #endif
//             }
//             catch (System.ArgumentException ex)
//             {
//                 Debug.LogWarning("parse json error: " + ex.Message + "\n\n" + json);
//             }
            }

        public virtual void FromOtherAsset(Asset other)
        {
            Debug.LogWarning("Child class of Asset should override FromOtherAsset()");
        }

        public virtual void FromString(string value)
        {

        }
        
        public virtual void RuntimeInit()
        {
			
        }
        
        public virtual void FromCSV(string csv)
        {

        }

        public string getAssetVariableName(bool replaceSuffix = false)
        {
            return StringToVariableName(name, replaceSuffix, suffixName);
        }
        public static string StringToVariableName(string name, bool replaceSuffix = false, string suffixName = "")
        {
            string variableName = "";
            var found = false;
            var _name = name;
            if (replaceSuffix)
            {
                _name = name.Replace(suffixName, string.Empty);
            }
            for (int k = 0; k < _name.Length; k++)
            {
                if (_name[k] == '.')
                {
                    found = true;
                    continue;
                }


                variableName += found ? _name[k].ToString().ToUpper() : _name[k].ToString();
                found = false;
            }
            return variableName;
        }

        [ContextMenu("Log asset name")]
        public void DebugAssetName()
        {
            Debug.Log(getAssetVariableName());
        }

#if UNITY_EDITOR
        [SerializeField] internal bool DebugChanged = false;
		[Multiline] [SerializeField] string Notes;
        public bool keepEditorValue = false;

        public virtual void OnDraw(Rect rect) {}
		#endif
		
		internal virtual void TriggerChange(){}

        [ContextMenu("ToJson")]
        public virtual string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
		
        public override string ToString() { return ToJson(); }

        public virtual string EditorAssetId { get { return string.Empty; } }

        public virtual Type getTypeT() { return GetType(); }


        public virtual void SetValue(string value) { }

        [ContextMenu("View Current Value")]
        public virtual void ViewCurrentValue() { }


        public virtual object getTValue()
        {
            return null;
        }



        //normal trigger
        public UnityEvent evt;
        public void AddListenner(UnityAction ac)
        {
	        evt.RemoveListener(ac);
            evt.AddListener(ac);
        }
        public void RemoveListenner(UnityAction ac)
        {
            evt.RemoveListener(ac);
        }
	}

	// public class AssetCollectionT<T>: Asset, ISerializationCallbackReceiver
	// {
	// 	[SerializeField] internal T[] listValue; // persistent value
	// 	internal int _index;
		

	// }
	
	public class AssetT<T> : Asset //, ISerializationCallbackReceiver
	{
        public bool ShouldCheckChange = true;
        [Serializable] public class TWrapper
        {
            [SerializeField] public T _value;
        }
        
        public override Type getTypeT()
        {
            return typeof(T);
        }

        [SerializeField] protected T _value; // persistent value
		
		// Add a getter / setter to notify changes
		[NonSerialized] protected T _current;

		
		public override void ViewCurrentValue()
        {
            _value = Value;

        }
		[NonSerialized] protected bool _inited = false;
		protected virtual void Init()
		{
			if (_inited)
			{
				Debug.LogWarning(name + " ---> Inited before");
				return;
			}
			
			_inited = true;

			if (mode == AssetMode.Runtime)
			{
				_current = typeof(T).IsClass ? JsonUtility.FromJson<T>("{}") : default(T);	
				//Debug.Log(name + " Init ---> " + _current);
			}

			if (parentAsset != null)
			{
				var p = (IParentAsset)parentAsset;
				//Debug.Log("parent != null:: try to bind --> " + p);
				if (p != null) p.ConnectChildren();
			}
		}
        public override object getTValue()
        {
            return Value;
        }
        public virtual T Value {
			get {
				if (!_inited) Init();
				return (mode == AssetMode.Persistent) ? _value : _current;
			}
			set
			{
				if (!_inited) Init(); // must also init to set default value for _current & set _inited to true!
#if UNITY_EDITOR
                if (keepEditorValue)
                {
                    Debug.LogWarning(name + ": don't change value in editor");
                    return;
                }

				UnityEditor.EditorUtility.SetDirty(this);
#endif

                if (!Application.isPlaying)
				{
					// set both
					ShowDebugChanged(_current, value);
					
					_value = value;
					_current = value;
					TriggerChange();
					return;
				}

				if (mode == AssetMode.Runtime)
				{
					if (ShouldCheckChange && EqualityComparer<T>.Default.Equals(_current, value)) return;
					ShowDebugChanged(_current, value);
					_current = value;
					TriggerChange();
					return;
				}
				
				if (mode == AssetMode.Persistent)
				{
					if (ShouldCheckChange && EqualityComparer<T>.Default.Equals(_value, value)) return;
					ShowDebugChanged(_value, value);
					_value = value;
					TriggerChange();
					return;
				}
				
				Debug.LogWarning("Something wrong - unsupported mode: " + mode);
			}
		}
        public override void FromOtherAsset(Asset other)
        {
            Value = JsonUtility.FromJson<T>(other.ToJson());
        }
        public override string ToJson()
        {   
            if (mode == AssetMode.Runtime && Application.isPlaying)
            {
                return JsonUtility.ToJson(_current);
            }
            
            return JsonUtility.ToJson(Value);
        }
        
        public override void FromJson(string json)
        {
	        if (string.IsNullOrEmpty((json)))
	        {
		        Debug.LogWarning("Invalid json - should not be null or empty!");
		        return;
	        }
	        
            try
            {
				Value = JsonUtility.FromJson<T>(json);
				
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
            catch (System.ArgumentException ex)
            {
                Debug.LogWarning("parse json error: " + ex.Message + "\n\n" + json);
            }
        }

        public override void FromString(string value)
        {
            if (string.IsNullOrEmpty((value)))
            {
                Debug.LogWarning("Invalid value - should not be null or empty!");
                return;
            }

            try
            {
                var obj = XReader.GetValue(getTypeT(), value);
                if(obj == null)
                {
	                Debug.LogError($"VariableAsset >>> Parse fail >>> value [{value}] || name [{name}] || type [{getTypeT()}]: ");
                    return;
                }
                Value = (T)obj;

#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
            catch (System.ArgumentException ex)
            {
                Debug.LogWarning("parse value error: " + ex.Message + "\n\n" + value);
            }
        }

        protected void NotifyChange()
        {
            TriggerChange();
        }
        
        private void ShowDebugChanged(T oldValue, T newValue)
        {
#if UNITY_EDITOR
            if (DebugChanged)
            {
                Debug.LogFormat("{0} :: change from[{1}] to[{2}]", name, oldValue, newValue);
            }
#endif
        }
        public override string ToString()
        {
            return Value != null ? Value.ToString() : name;
        }
        
        public void ForceSave()
        {
	        FileHelper.SaveFileWithPassword(this.ToJson(), this.getAssetVariableName());
        }
//#if UNITY_EDITOR

//        private HashSet<Action<T>> _onChange2 = new HashSet<Action<T>>();

//        internal override void TriggerChange()
//        {
//            if(evt != null)
//            {
//                evt.Invoke();
//            }

//            if (_onChange2 == null) return;

//            // swap order
//            var arr = _onChange2.ToArray();
//            var arr1 = new List<int>();
//            for (int i  = 0; i < arr.Length; i++)
//            {
//                arr1.Add(i);
//            }

//            for (int i = arr1.Count - 1; i >= 0; i--)
//            {
//                var index = UnityEngine.Random.Range(0, arr1.Count);
//                arr[arr1[index]](Value);
//                arr1.RemoveAt(index);
//            }
//        }


//        public void AddListener(Action<T> callback)
//        {
//            if (callback == null) return;
//            _onChange2.Remove(callback);
//            _onChange2.Add(callback);
//        }

//        public void RemoveListener(Action<T> callback)
//        {
//            if (callback == null) return;
//            _onChange2.Remove(callback);
//        }
//#else
        internal override void TriggerChange()
		{
            try
            {
	            _onChange?.Invoke(Value);

	            evt?.Invoke();
            }
            catch(Exception ex)
            {
                Debug.LogError(this + ".TriggerChange() exception: --> " + ex);
            }
        }

		// Runtime binding: Event support
		private Action<T> _onChange;
		public void AddListener(Action<T> callback)
		{
			if (callback == null) return;
			_onChange -= callback;
			_onChange += callback;
		}

		public void RemoveListener(Action<T> callback)
		{
			if (callback == null) return;
			_onChange -= callback;
		}
//#endif

        // public void OnBeforeSerialize() {}
        // public void OnAfterDeserialize()
        // {
        //     if (DebugChanged) Debug.LogWarning("OnAfterDeserialize::  " );

        //     _current = _value;
        // }
        public override void SetValue(string value)
        {
            object v = T70_Helper.GetValue(value, getTypeT());
            if (v == null) return;

#if !UNITY_EDITOR
            try
#endif
            {
                Value = (T)v;
            }
#if !UNITY_EDITOR
            catch { }
#endif
        }
        public static implicit operator T (AssetT<T> asset) { return asset != null ? asset.Value : default(T); }
	}

	// ----------------- VARIABLES --------------------

	public enum VariableMode { Constant, Asset }
	[Serializable] public class Variable<T, T2>  where T2: AssetT<T>
	{
		public VariableMode mode;
		[SerializeField] protected T constValue;
		[SerializeField] protected T2 assetValue;
		
		public T Value { get { return mode == VariableMode.Constant ? constValue : assetValue.Value; }}

		// public static implicit operator T1 (T2 asset) { return asset.Value; }
	}

	
}