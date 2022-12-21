using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DefaultNamespace;
using UnityEngine;

public class Paralax : MonoBehaviour, ITickLate
{
    private Transform _cameraTransform;
    private ParalaxLayer[] _layers;
    private bool _isMoving;

    private void Start()
    {
        ManagerUpdate.AddTo(this);
        _cameraTransform = Camera.main.transform;
        _layers = GetComponentsInChildren<ParalaxLayer>();
    }

    // public void MoveLayers(int direction)
    // {
    //     foreach (var VARIABLE in _layers)
    //     {
    //         VARIABLE.MoveLayer(_cameraTransform.position.x);
    //     }
    // }
    public void SetMoving(bool moving)
    {
        _isMoving = moving;
    }

    public void TickLate()
    {
        if (_isMoving == false) return;
        foreach (var VARIABLE in _layers)
        {
            VARIABLE.MoveLayer(_cameraTransform.position.x);
        }
    }
}