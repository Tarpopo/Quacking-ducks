using System.Collections.Generic;

public static class ListExtensions
{
    public static T Random<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
}