using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/EasyLevel")]
public class EasyLevel : ScriptableObject
{
    public Dictionary<GameObject,List<Vector3>> AllMapElements=new Dictionary<GameObject, List<Vector3>>();
    
    //visible inspector elements
    public List<Vector3> GameObjects;
    public void WriteList()
    {
        GameObjects=new List<Vector3>();
        foreach (var dictionary in AllMapElements)
        {
            foreach (var list in dictionary.Value)
            {
                GameObjects.Add(list);
            }
        }
    }
}
