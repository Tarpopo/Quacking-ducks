using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class DataContainer : ManagerBase, IAwake
{
    public DataActor CurrentDuck => _ducks.GetElementByEnum(_currentDuck.Value);
    public WeaponData[] WeaponData => _weaponData;
    [SerializeField] private DataActor[] _ducks;
    [SerializeField] private WeaponData[] _weaponData;
    private SavableEnum<Ducks> _currentDuck;

    public void OnAwake()
    {
        _ducks = _ducks.SortByEnum().ToArray();
        _currentDuck = new SavableEnum<Ducks>(nameof(DataContainer), Ducks.Rambo);
    }

    public void SetCurrentDuck(Ducks duck)
    {
        _currentDuck.Value = _ducks.GetElementByEnum(duck).EnumValue;
        _currentDuck.Save();
    }
}