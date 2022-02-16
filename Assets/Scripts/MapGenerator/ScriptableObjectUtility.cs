using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class ScriptableObjectUtility
{
    public static void CreateAsset<T> (T prefab, string assetPath="Assets") where T : ScriptableObject
    {
        var path = assetPath + "/" + SceneManager.GetActiveScene().name + ".asset";
        var asset = (T)AssetDatabase.LoadAssetAtPath(path,typeof(T));
        if (asset!=null)
        {
            Debug.Log("Exist");
            AssetDatabase.DeleteAsset(path);
        }
        
        ScriptableObject.CreateInstance<T> ();
        asset=prefab;
        
        AssetDatabase.CreateAsset (asset, path);
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow ();
        Selection.activeObject = asset;
    }
}
