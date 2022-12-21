using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class DataContainer : ManagerBase, IAwake
{
    public DataActor[] DucksData => _ducks;
    public WeaponData[] WeaponData => _weaponData;
    [SerializeField] private DataActor[] _ducks;
    [SerializeField] private WeaponData[] _weaponData;
    public void OnAwake() => _ducks = _ducks.SortByEnum().ToArray();
}