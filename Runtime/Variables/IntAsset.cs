
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "Int Asset", menuName = VariableConst.MenuPath.Int, order = VariableConst.Order.Int)]
	public class IntAsset : AssetT<int>, IVariationIndex 
	{
		public int min;
		public int max;

		#if UNITY_EDITOR
		public override void OnDraw(Rect rect) 
		{
			var v = Value;

			if (min != max)
			{
				v = UnityEditor.EditorGUI.IntSlider(rect, string.Empty, Value, min, max);
			} else {
				v = UnityEditor.EditorGUI.IntField(rect, string.Empty, Value);
			}

			if (v != Value) Value = v;
		}
		#endif

		public static IntAsset operator ++(IntAsset asset)
		{
			asset.Value++;
			return asset;
		}

		public static IntAsset operator --(IntAsset asset)
		{
			asset.Value--;
			return asset;
		}

		public override string EditorAssetId
        {
            get { return "Editor_VIntSlider"; }
        }

		public int GetVariationIndex()
		{
			return (int)Value;
		}
	}

	public partial class V
	{ 
		[Serializable] public class Int : Variable<int, IntAsset> 
		{
			public static implicit operator int (Int variable) { return variable.Value; }
		}
	}
}