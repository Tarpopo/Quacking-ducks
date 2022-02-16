using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Managers/SceneController")]
public class SceneController : ManagerBase
{
   public void LoadScene(int sceneNumber)
   {
      SceneManager.LoadScene(sceneNumber);
   }

   public void LoadMenuScene()
   {
      SceneManager.LoadScene(0);
   }

   public bool GetIsMainScene()
   {
      return SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0);
   }
}
