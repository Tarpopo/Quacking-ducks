using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Shop : ManagerBase, IAwake
{
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private int _coins = 999;

    public void OnAwake()
    {
    }

    private void OnEnable()
    {
    }

    public bool TryReduceCoins(int coins)
    {
        if (_coins - coins < 0) return false;
        _coins -= coins;
        _coinText.text = _coins.ToString();
        return true;
    }

    public void UpdateCoinText()
    {
        // _coinText.text = Toolbox.Get<PlayerStats>().CoinCount.ToString();
    }

    public void ActiveShopItem(int index)
    {
        _shopItems[index].IsBuy = true;
        _shopItems[index].TriggerButton.IsActive = true;
        UpdateCoinText();
        // Toolbox.Get<Save>()._save.ShopItems = _shopItems;
        Toolbox.Get<Save>().SaveAll();
    }
}

[Serializable]
public class ShopItem
{
    public TriggerButton TriggerButton;
    public bool IsBuy;
}