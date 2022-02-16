using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerChck : MonoBehaviour
{
    public static readonly Func<int, int, int> SumMemoized = Memorizer.Memorize<int, int, int>(Sum);
    public static readonly Func<Vector3,Vector3, float> DistanceMemorizer =
        Memorizer.Memorize<Vector3,Vector3, float>(GetDistance);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(Selection.activeObject);
        }
    }

    private static int Sum(int a, int b)
    {
        return a + b;
    }

    private static float GetDistance(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }

    private void Start()
    {
        print(SumMemoized(5,5));
        print(SumMemoized(5,5));
        print(SumMemoized(5,6));
    }
    
}
