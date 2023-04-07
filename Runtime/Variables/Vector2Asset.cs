
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVector2 = UnityEngine.Vector2;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "Vector2 Asset", menuName = VariableConst.MenuPath.Vector2, order = VariableConst.Order.Vector2)]
	public class Vector2Asset : AssetT<UnityVector2> 
	{
		#if UNITY_EDITOR
		public override void OnDraw(Rect rect) 
		{
			Value = UnityEditor.EditorGUI.Vector2Field(rect, string.Empty, Value);
		}
		#endif
	}

	public partial class V
	{ 
		[Serializable] public class Vector2 : Variable<UnityVector2, Vector2Asset> 
		{
			public static implicit operator UnityVector2 (Vector2 variable) { return variable.Value; }
		}
	}
}