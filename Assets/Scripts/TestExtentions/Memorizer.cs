using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorizer : MonoBehaviour
{
    public static Func<TReturn> Memorize<TReturn>(Func<TReturn> func)
    {
        object obj = null;
        return () =>
        {
            if (obj == null) obj = func();
            return (TReturn)obj;
        };
    }

    public static Func<TSource, TReturn> Memorize<TSource, TReturn>(Func<TSource, TReturn> func)
    {
        Dictionary<TSource, TReturn> dictionary = new Dictionary<TSource, TReturn>();

        return (s) =>
        {
            if (dictionary.ContainsKey(s) == false) dictionary[s] = func(s);
            return dictionary[s];
        };
    }

    public static Func<TSource1, TSource2, TReturn> Memorize<TSource1, TSource2, TReturn>(
        Func<TSource1, TSource2, TReturn> func)
    {
        Dictionary<string, TReturn> dictionary = new Dictionary<string, TReturn>();
        print("sum");
        return (s1, s2) =>
        {
            var hashCode = (s1.GetHashCode() + s2.GetHashCode()).ToString();
            if (dictionary.ContainsKey(hashCode) == false) dictionary[hashCode] = func(s1, s2);
            return dictionary[hashCode];
        };
    }
}