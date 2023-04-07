
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "String Asset", menuName = VariableConst.MenuPath.String, order = VariableConst.Order.String)]
	public class StringAsset : AssetT<string> 
	{
		#if UNITY_EDITOR
		public override void OnDraw(Rect rect) 
		{
			Value = UnityEditor.EditorGUI.TextArea(rect, Value, GUIStyle.none);
		}
		#endif
	}
	
	public partial class V
	{ 
		[Serializable] public class String : Variable<string, StringAsset> 
		{
			public static implicit operator string (String variable) { return variable.Value; }
		}
	}
}