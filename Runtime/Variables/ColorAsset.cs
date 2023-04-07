using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityColor = UnityEngine.Color;

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "Color Asset", menuName = VariableConst.MenuPath.Color, order = VariableConst.Order.Color)]
	public class ColorAsset : AssetT<UnityColor> 
	{
		#if UNITY_EDITOR
		void Awake()
		{
			if (Value == new Color(0,0,0,0)) Value = Color.white;
		}
		
		public override void OnDraw(Rect rect) 
		{
			Value = UnityEditor.EditorGUI.ColorField(rect, Value);
		}
		#endif
	}

	public partial class V
	{ 
		[Serializable] public class Color : Variable<UnityColor, ColorAsset> 
		{
			public static implicit operator UnityColor (Color variable) { return variable.Value; }
		}
	}
}