using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    private Lift[] _lifts;

    private void Start()
    {
        _lifts = FindObjectsOfType<Lift>();
    }

    public void ResetLifts()
    {
        foreach (var lift in _lifts)
        {
            lift.SetStartState();
        }
    }
}