using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using T70.VariableAsset;

public class AssetDrawer<T> : PropertyDrawer where T: Asset
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
		label		= EditorGUI.BeginProperty(position, label, property);
		position	= EditorGUI.PrefixLabel(position, label);
		var rect 	= position;
		rect.height = 16f;
        SerializedProperty prop = null; 
		EditorGUI.BeginChangeCheck();
		{
			var modeProp = property.FindPropertyRelative("mode");
			var mode = (VariableMode) modeProp.enumValueIndex;
			var mode2 = (VariableMode)EditorGUI.EnumPopup(rect, mode);

			if (mode2 != mode)
			{
				mode = mode2;
				modeProp.enumValueIndex = (int)mode2;
			}
			
			rect.y += 18f;
			if (mode == VariableMode.Constant)
			{
				DrawConstant(rect, property.FindPropertyRelative("constValue"));
			}

			if (mode == VariableMode.Asset)
			{
                prop = property.FindPropertyRelative("assetValue");
				EditorGUI.PropertyField(rect, prop, GUIContent.none);
				rect.y += 18f;
				DrawScriptable(rect, (T)prop.objectReferenceValue);
			}


		}
		if(EditorGUI.EndChangeCheck())
        {
            if(prop != null && prop.objectReferenceValue != null)
            {
                if(prop.serializedObject != null && prop.serializedObject != null)
                EditorUtility.SetDirty(prop.serializedObject.targetObject);
            }
        }
	}

	virtual internal void DrawConstant(Rect rect, SerializedProperty prop)
	{
		EditorGUI.PropertyField(rect, prop, GUIContent.none);
	}

	virtual internal void DrawScriptable(Rect rect, T obj)
	{
		if (obj == null) return;
		obj.OnDraw(rect);
	}

	override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		var modeProp = property.FindPropertyRelative("mode");
		var mode = (VariableMode)modeProp.enumValueIndex;
		return mode == VariableMode.Constant ? 36f : 54;
	}
}


public class AssetEditor<T> : Editor where T : Asset
{
	override public void OnInspectorGUI() 
	{
		var t = (T) target;
		if (t == null) return;

		var rect = GUILayoutUtility.GetRect(0, Screen.width, 18f, 18f);
		t.OnDraw(rect);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		DrawDefaultInspector();
	}
}



[CustomEditor(typeof(EventAsset))]
public class EventEditor : Editor
{
		public override void OnInspectorGUI()
		{
				base.OnInspectorGUI();

				GUI.enabled = Application.isPlaying;
				EventAsset e = target as EventAsset;
				if (GUILayout.Button("Raise"))
				{
					e.Raise();
				}
		}
}


// -----------------

[CustomPropertyDrawer(typeof(V.Float))] public class FloatAssetDrawer : AssetDrawer<FloatAsset> {}
[CustomEditor(typeof(FloatAsset))] public class FloatAssetEditor : AssetEditor<FloatAsset> {}


[CustomPropertyDrawer(typeof(V.Int))] public class IntAssetDrawer : AssetDrawer<IntAsset> {}
[CustomEditor(typeof(IntAsset))] public class IntAssetEditor : AssetEditor<IntAsset> {}


[CustomPropertyDrawer(typeof(V.String))] public class StringAssetDrawer : AssetDrawer<StringAsset> {}
[CustomEditor(typeof(StringAsset))] public class StringAssetEditor : AssetEditor<StringAsset> {}


[CustomPropertyDrawer(typeof(V.Bool))] public class BoolAssetDrawer : AssetDrawer<BoolAsset> {}
[CustomEditor(typeof(BoolAsset))] public class BoolAssetEditor : AssetEditor<BoolAsset> {}


[CustomPropertyDrawer(typeof(V.Vector2))] public class Vector2AssetDrawer : AssetDrawer<Vector2Asset> {}
[CustomEditor(typeof(Vector2Asset))] public class Vector2AssetEditor : AssetEditor<Vector2Asset> {}

// [CustomPropertyDrawer(typeof(V.Color))] public class ColorAssetDrawer : AssetDrawer<ColorAsset> {}
// [CustomEditor(typeof(ColorAsset))] public class ColorAssetEditor : AssetEditor<ColorAsset> {}