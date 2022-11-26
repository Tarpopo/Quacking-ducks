using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    [SerializeField] private List<ManagerBase> _managers;

    private void Awake()
    {
        foreach (var managerBase in _managers) Toolbox.Add(managerBase);

        SceneManager.sceneLoaded += Toolbox.Instance.SceneChanged;
        SceneManager.sceneUnloaded += Toolbox.Instance.ClearScene;
    }
}