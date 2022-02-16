using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MapCreator : EditorWindow
{
    private readonly string _mapAssetPath="Assets/Data/MapAssets";
    private string _prefabPath = "Assets/Prefabs/Map";
    
    [MenuItem("Window/MapCreator")]
    public static void  ShowWindow () 
    {
        GetWindow(typeof(MapCreator));
    }
    
    public void OnGUI()
    {
        if (GUILayout.Button("Create Level Asset"))
        {
            FolderCreator.CreateFolder(_mapAssetPath);
            var levelAsset = new EasyLevel();
            var objects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var obj in objects)
            {
                var prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj);
                if (prefab == null) continue;
                Debug.Log(prefab.name);
                
                if (levelAsset.AllMapElements.ContainsKey(prefab)==false)
                {
                    levelAsset.AllMapElements.Add(prefab,new List<Vector3>());
                }
                levelAsset.AllMapElements[prefab].Add(obj.transform.position);
            }
            levelAsset.WriteList();
            ScriptableObjectUtility.CreateAsset(levelAsset,_mapAssetPath);
        }

        if (GUILayout.Button("CreateFolder"))
        {
            FolderCreator.CreateFolder(_prefabPath);
        }

        if (GUILayout.Button("CreatePrefab"))
        {
            FolderCreator.CreateFolder(_prefabPath);
            GameObject[] objectArray = Selection.gameObjects;
            
            foreach (GameObject gameObject in objectArray)
            {
                if(ValidateCreatePrefab(gameObject)==false)continue;
                string localPath = _prefabPath+"/"+ gameObject.name + ".prefab";
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
                PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, InteractionMode.UserAction);
            }
        }
        
    }
    private bool ValidateCreatePrefab(GameObject gameObject)
    {
        return gameObject != null && !EditorUtility.IsPersistent(gameObject)&&!PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
    }

}
