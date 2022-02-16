using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class FolderCreator
    {
        public const string RootFolder = "Assets";
    
        public static void CreateFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            var splitPath = path.Split('/');
            var newSplitPath = RootFolder;
        
            for (var i = 1; i < splitPath.Length; i++)
            {
                if (splitPath[i] == "") continue;
                if (AssetDatabase.IsValidFolder(newSplitPath+"/"+splitPath[i]))
                {
                    newSplitPath += "/" + splitPath[i];
                    continue;
                }
                AssetDatabase.CreateFolder(newSplitPath,splitPath[i]);
                newSplitPath += "/" + splitPath[i];
            }
        }
    
    }
}
