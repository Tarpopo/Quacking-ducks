using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneComponent : MonoBehaviour
{
   private GameObject _gameObject;
   public void ActiveGOWtihTime(GameObject gameObject, float time)
   {
      _gameObject = gameObject;
      Invoke(nameof(ActiveGameObject),time);
   }
   private void ActiveGameObject()
   {
      _gameObject.SetActive(true);
   }

   public void LoadMainSceneWithTime(float time)
   {
      Invoke(nameof(LoadTransition),time/2);
      Invoke(nameof(LoadMainScene), time);
   }

   private void LoadTransition()
   {
      // FindObjectOfType<Transition>().PlayGateDown();
   }

   private void LoadMainScene()
   {
      Toolbox.Get<SceneController>().LoadMenuScene();
   }

}
