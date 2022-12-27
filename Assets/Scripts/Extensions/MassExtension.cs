using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class MassExtension
{
    // public static T[] SortByEnum<T>(this T[] mass) where T : IEnum
    // {
    //     var newMass = new T[mass.Length];
    //     foreach (var value in mass)
    //     {
    //         newMass[Convert.ToInt32(value.EnumValue)] = value;
    //     }
    //
    //     return newMass;  
    // }
    public static IEnumerable<T> SortByEnum<T>(this IEnumerable<T> mass) where T : IEnum =>
        mass.OrderBy(value => Convert.ToInt32(value.EnumValue)).ToArray();

    public static T GetElementByEnum<T>(this T[] mass, Enum enumValue) where T : IEnum =>
        mass[Convert.ToInt32(enumValue)];

    public static T GetRandom<T>(this IEnumerable<T> mass)
    {
        var enumerable = mass as T[] ?? mass.ToArray();
        return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
    }

    public static Dictionary<Enum, T> ConvertToDictionary<T>(this IEnumerable<T> mass) where T : IEnum =>
        mass.ToDictionary(item => item.EnumValue);
}

public interface IEnum
{
    Enum EnumValue { get; }
}