using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Managers/Stats")]
public class PlayerStats : ManagerBase, IAwake
{
    public int CoinCount;
    public void OnAwake()
    {
        CoinCount = Toolbox.Get<Save>()._save.MoneyCount;
    }
}
