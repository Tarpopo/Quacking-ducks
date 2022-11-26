using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Shop : ManagerBase, IAwake, ISceneChanged
{
    [SerializeField] private List<ShopItem> _shopItems;
    private TMP_Text _coinText;

    public void OnChangeScene()
    {
        // if (Toolbox.Get<SceneController>().GetIsMainScene() == false) return;
        // _shopItems[0].TriggerButton.transform.parent.gameObject.SetActive(false);
    }

    public void OnAwake()
    {
        if (Toolbox.Get<Save>()._save.ShopItems.Count != 0)
        {
            _shopItems = Toolbox.Get<Save>()._save.ShopItems;
        }

        _coinText = GameObject.FindWithTag("Coin").GetComponentInChildren<TMP_Text>();
        UpdateCoinText();
        var objects = GameObject.FindWithTag("Shop").GetComponentsInChildren<ShopCell>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (_shopItems[i].IsBuy)
            {
                objects[i].IsActive = true;
                if (i == 0) continue;
                var shopCell = objects[i].GetComponent<ShopCell>();
                shopCell.Gate.SetActive(false);
                shopCell._lockObj.SetActive(false);
                objects[i].gameObject.GetComponent<TriggerButton>().IsActive = true;
            }

            _shopItems[i].TriggerButton = objects[i].gameObject.GetComponent<TriggerButton>();
        }

        objects[0].transform.parent.gameObject.SetActive(false);
    }

    public void UpdateCoinText()
    {
        _coinText.text = Toolbox.Get<PlayerStats>().CoinCount.ToString();
    }

    public void ActiveShopItem(int index)
    {
        _shopItems[index].IsBuy = true;
        _shopItems[index].TriggerButton.IsActive = true;
        UpdateCoinText();
        Toolbox.Get<Save>()._save.ShopItems = _shopItems;
        Toolbox.Get<Save>().SaveAll();
    }
}

[Serializable]
public class ShopItem
{
    public TriggerButton TriggerButton;
    public bool IsBuy;
}