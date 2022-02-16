using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropeSpawner : MonoBehaviour
{
   [SerializeField] private SceneItem[] _props;
   private Vector3[] _startPositions;

   private void Start()
   {
      _startPositions = new Vector3[_props.Length];
      for (int i = 0; i < _props.Length; i++)
      {
         _startPositions[i] = _props[i].transform.localPosition;
      }
   }

   public void CheckAllProps()
   {
      for (int i = 0; i < _props.Length; i++)
      {
         _props[i].transform.localPosition = _startPositions[i];
         if (_props[i].gameObject.activeSelf) continue;
         _props[i].gameObject.SetActive(true);
         _props[i].OnSpawn();
      }
   }
}
