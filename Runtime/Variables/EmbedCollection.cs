using System.Collections;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;
using UnityEngine;
using T70.VariableAsset;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(EmbedCollection))]
public class EmbedCollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Add Current Selection To Embed"))
        {
            EmbedCollection m_target = target as EmbedCollection;
            var guids = Selection.assetGUIDs;
            List<Object> obj = new List<Object>();
            for(int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);


                obj.Add(AssetDatabase.LoadAssetAtPath<Object>(path));
            }
            m_target.Add(obj);
        }
    }
}

#endif

[CreateAssetMenu(fileName = "EmbedCollection", menuName = VariableConst.MenuPath.EmbedCollection, order = VariableConst.Order.EmbedCollection)]
public class EmbedCollection : ScriptableObject
{
    [Serializable]
    public class EmbedItem
    {
        public string name;
        public Object target;
        public int version = 1;
    }

    [SerializeField]
    private List<EmbedItem> listEmbed = new List<EmbedItem>();
    
    public void Add(List<Object> objects)
    {
        for(int i = 0; i < objects.Count; i++)
        {
            listEmbed.Add
            (
                new EmbedItem
                {
                    #if UNITY_EDITOR
                    name = objects[i].name,
                    #endif
                    target = objects[i]
                }
            );
        }

    }

    private Dictionary<string, EmbedItem> m_DictEmbed;
    public Dictionary<string, EmbedItem> DictEmbed
    {
        get
        {
            InitIfNeeded();
            return m_DictEmbed;
        }
    }
    private void InitIfNeeded()
    {
        if (m_DictEmbed != null) return;
        m_DictEmbed = new Dictionary<string, EmbedItem>();
        for (int i = 0; i < listEmbed.Count; i++)
        {
            var item = listEmbed[i];
            // if (item.target == null)
            // {
            //     Debug.LogWarning("target null element index: " + i);
            //     continue;
            // }

            // var targetName = item.target.name;
            // if(m_DictEmbed.ContainsKey(targetName))
            // {
            //     Debug.LogWarning("Duplicated item: " + targetName);
            //     continue;
            // }
            // m_DictEmbed.Add(targetName, item);
            var targetName = item.name;
            if(m_DictEmbed.ContainsKey(targetName))
            {
                Debug.LogWarning("Duplicated item: " + targetName);
                continue;
            }
            m_DictEmbed.Add(targetName, item);
            
        }
    }

    [ContextMenu("Clean up")]
    public void ClearMissing()
    {
        for (int i = listEmbed.Count - 1; i >= 0; i--)
        {
            var item = listEmbed[i];
            if (item.target == null)
            {
                listEmbed.RemoveAt(i);
            }
        }
    }

    public bool CheckEmbedByName(string fileName, int version)
    {
        if (!DictEmbed.ContainsKey(fileName)) return false;

        var embedData = DictEmbed[fileName];

        if (embedData.version < version) return false;
        
        return true;
    }

    public Object TryGetData(string fileName, int version)
    {
        if (!DictEmbed.ContainsKey(fileName)) return null;

        var embedData = DictEmbed[fileName];

        if (embedData.version < version) return null;

        return embedData.target;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying) return;

        if (listEmbed == null) return;
        
        listEmbed.Sort((item1, item2) => String.Compare(item1.name, item2.name, StringComparison.Ordinal));
        
        for(int  i = 0; i < listEmbed.Count; i++)
        {
            if(listEmbed[i].target == null)
            {
                Debug.LogWarning("target null element index: " + i);
                continue;
            }

            listEmbed[i].name = listEmbed[i].target.name;
        }
    }
#endif

}
