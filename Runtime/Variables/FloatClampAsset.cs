using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "Float Asset Clamp", menuName = VariableConst.MenuPath.AbTest_Collection, order = VariableConst.Order.AbTest_Collection)]
    public class FloatClampAsset : AssetT<float>
    {

        public float min;
        public float max;
#if UNITY_EDITOR
        public override void OnDraw(Rect rect)
        {
            EditorGUI.BeginChangeCheck();
            {
                if (min != max)
                {
                    Value = UnityEditor.EditorGUI.Slider(rect, string.Empty, Value, min, max);
                }
                else
                {
                    Value = UnityEditor.EditorGUI.FloatField(rect, string.Empty, Value);
                }
            }
            if (EditorGUI.EndChangeCheck())
            {
                Debug.Log("Changed!");
                EditorUtility.SetDirty(this);
            }
        }

#endif
    }

    public partial class V
    {
        [Serializable]
        public class FloatClamp : Variable<float, FloatClampAsset>
        {
            public static implicit operator float (FloatClamp variable) { return variable.Value; }
        }
    }
}
