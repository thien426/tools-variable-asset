
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace T70.VariableAsset
{
	[CreateAssetMenu(fileName = "Float Asset", menuName = VariableConst.MenuPath.Float, order = VariableConst.Order.Float)]
	public class FloatAsset : AssetT<float> 
	{
		
		public float min;
		public float max;

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
            if(float.TryParse(json, NumberStyles.Float, CultureInfo.InvariantCulture, out float f))
            {
                Value = f;
            }

        }
#if UNITY_EDITOR

        public override void OnDraw(Rect rect) 
		{
			EditorGUI.BeginChangeCheck();
			{
				if (min != max)
				{
					Value = UnityEditor.EditorGUI.Slider(rect, string.Empty, Value, min, max);
				} else {
					Value = UnityEditor.EditorGUI.FloatField(rect, string.Empty, Value);
				}
			}
			if (EditorGUI.EndChangeCheck()) 
			{
				EditorUtility.SetDirty(this);
			}
		}
		#endif
		
        public override string EditorAssetId
        {
            get { return "Editor_VSlider"; }
        }
    }

	public partial class V
	{ 
		[Serializable] public class Float : Variable<float, FloatAsset> 
		{
			public static implicit operator float (Float variable) { return variable.Value; }
		}
	}
}