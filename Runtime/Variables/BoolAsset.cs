
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "Boolean Asset", menuName = VariableConst.MenuPath.BOOL_ASSET, order = VariableConst.Order.BOOL_ASSET)]
	public class BoolAsset : AssetT<bool>, IVariationIndex
	{

        public override string ToJson()
        {
            if (mode == AssetMode.Runtime && Application.isPlaying)
            {
                return _current.ToString();
            }

            return Value.ToString();
        }
        public override void FromJson(string json)
        {
            if (bool.TryParse(json, out bool b))
            {
                Value = b;
            }

        }

#if UNITY_EDITOR
        public override void OnDraw(Rect rect)
        {
	        var v1 = Value;
			var v2 = UnityEditor.EditorGUI.Toggle(rect, v1);
			if (v2 != v1)
			{
				Value = v2;
			}
		}
		#endif

		public override string EditorAssetId
        {
            get { return "Editor_VToggle"; }
        }

		public int GetVariationIndex()
		{
			return Value ? 1 : 0;
		}
	}

	public partial class V
	{ 
		[Serializable] public class Bool : Variable<bool, BoolAsset> 
		{
			public static implicit operator bool (Bool variable) { return variable.Value; }
		}
	}
}