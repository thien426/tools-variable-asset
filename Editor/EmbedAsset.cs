using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(EmbedAsset))]
public class EmbedAssetEditor : Editor
{
    private bool isDownloading = false;
    private float progress;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!isDownloading)
        {
            if(GUILayout.Button("Embed"))
            {
                isDownloading = true;
                progress = 0f;
                downloadIndex = 0;
                EditorApplication.update -= Update;
                EditorApplication.update += Update;
            }
        }
        else
        {
            var rect = EditorGUILayout.GetControlRect();
            EditorGUI.ProgressBar(rect, progress, (progress * 100) + "%");
        }

    }
    WWW www;
    private int downloadIndex = 0;
    private void Update()
    {
        if (!isDownloading) return;
        var embed = target as EmbedAsset;
        var asset = embed.lstAsset[downloadIndex];
        if (www == null)
        {
            string url = string.Format(asset.url, embed.configVersion);
            Debug.Log("Begin embed " + asset.overrideAsset.name + " url: " + url);
            www = new WWW(url);
        }


        progress = downloadIndex * 1f / embed.lstAsset.Count + www.progress;
        Repaint();

        if (www.isDone || !string.IsNullOrEmpty(www.error))
        {
            if (!string.IsNullOrEmpty(www.error))
            {

                Debug.LogError("Download error: " + www.error);
            }
            else
            {

                var path = AssetDatabase.GetAssetPath(asset.overrideAsset);
                System.IO.File.WriteAllText(path, www.text);

                Debug.Log("Embed Success: " + asset.overrideAsset.name);
            }
            downloadIndex++;
            www = null;
            if (downloadIndex >= embed.lstAsset.Count)
            {
                //complete
                AssetDatabase.Refresh();
                Debug.Log("Embed Completed");
                www = null;
                isDownloading = false;
                EditorApplication.update -= Update;
                return;
            }


        }



    }
}
#endif

[CreateAssetMenu(fileName = "Embed Asset" , menuName = "Assets/Embed Asset")]
public class EmbedAsset : ScriptableObject
{
    [SerializeField] private string configVersionAndroid = "b114a";
    [SerializeField] private string configVersionIos = "b114a";

    public string configVersion
    {
        get
        {
#if UNITY_ANDROID
            return configVersionAndroid;
#endif
            return configVersionIos;
        }
    }
    public List<Asset> lstAsset;



    [Serializable]
    public class Asset
    {
        public string url;
        public TextAsset overrideAsset;
    }
}
