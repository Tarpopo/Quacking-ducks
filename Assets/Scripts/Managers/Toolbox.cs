using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toolbox : Singleton<Toolbox>
{
   private Dictionary<Type,object>data =new Dictionary<Type, object>();
   public void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
   {
      foreach (var obj in data)
      {
         var changed = obj.Value as ISceneChanged;
         changed?.OnChangeScene();
      }
   }

   public static void UpdateAllAwake()
   {
      foreach (var manager in Toolbox.Instance.data)
      {
         var obj = manager.Value as IAwake;
         obj?.OnAwake();
      }
   }

   public static void Add(object obj)
   {
      var add = obj;
       var manager = obj as ManagerBase;
       if (manager != null)
          add = Instantiate(manager);
      else return;
       Instance.data.Add(obj.GetType(), add);
      
      if (add is IAwake)
      {
         (add as IAwake).OnAwake();
      }
   }

   public static T Get<T>()
   {
      Instance.data.TryGetValue(typeof(T), out var resolve);
      return (T) resolve;
   }

   public void ClearScene(Scene scene)
   {
      foreach (var obj in data)
      {
         var perem = obj.Value as ManagerBase;
         perem?.ClearScene();
      }
   }
}
