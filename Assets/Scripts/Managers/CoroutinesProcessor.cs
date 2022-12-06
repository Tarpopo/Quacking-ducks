using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CoroutinesProcessor : ManagerBase
{
    //private readonly Dictionary<int, List<Coroutine>> _coroutines = new Dictionary<int, List<Coroutine>>(10);

    public void PlayCoroutine(int hashCode, IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
        // if (_coroutines.ContainsKey(hashCode) == false)
        // {
        //     _coroutines.Add(hashCode, new List<Coroutine>());
        //     _coroutines[hashCode].Add(StartCoroutine(enumerator));
        //     return;
        // }
        //
        // _coroutines[hashCode].Add(StartCoroutine(enumerator));
    }

    public void RemoveCoroutine(int hashCode)
    {
    }
}