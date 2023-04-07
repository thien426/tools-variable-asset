
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityVector3 = UnityEngine.Vector3;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace T70.VariableAsset
{
    [CreateAssetMenu(fileName = "Vector3 Asset", menuName = VariableConst.MenuPath.AbTest_Collection, order = VariableConst.Order.AbTest_Collection)]
    public class Vector3Asset : AssetT<UnityVector3>
    {
#if UNITY_EDITOR
        public override void OnDraw(Rect rect)
        {
            Value = UnityEditor.EditorGUI.Vector3Field(rect, string.Empty, Value);
        }
#endif
    }

    public partial class V
    {
        [Serializable]
        public class Vector3 : Variable<UnityVector3, Vector3Asset>
        {
            public static implicit operator UnityVector3(Vector3 variable) { return variable.Value; }
        }
    }
}