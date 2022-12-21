using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour, ITick
{
    private ItemsSpawner _itemsSpawner;
    private SceneItem _item;
    private int _coinCount;

    private int CoinCount
    {
        get => _coinCount;
        set
        {
            if (_coinCount <= 999) _coinCount = value;
        }
    }

    private Transform _transform;
    private float _currentTextTime;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _coin;

    [SerializeField] private LayerMask _player;

    //[SerializeField] private string _coinName;
    //[SerializeField] private Text _text;
    [SerializeField] private float _textTime;

    private void Start()
    {
        ManagerUpdate.AddTo(this);
        _transform = GetComponent<Transform>();
        _itemsSpawner = Toolbox.Get<ItemsSpawner>();
        _coin.SetActive(false);
        // CoinCount += Toolbox.Get<PlayerStats>().CoinCount;
    }

    public void Tick()
    {
        var coin = Physics2D.OverlapCircle(_transform.position, 0.04f, _player);
        if (coin && _itemsSpawner.Items.TryGetValue(coin.gameObject, out _item))
        {
            _item.TakeItem(null, Vector3.zero);
            CoinCount++;
            UpdateText(CoinCount);
        }

        if (_currentTextTime > 0)
        {
            _currentTextTime -= Time.deltaTime;
            if (_currentTextTime <= 0) _coin.SetActive(false);
        }
    }

    private void UpdateText(int coin)
    {
        _coin.SetActive(true);
        _text.text = coin.ToString();
        _currentTextTime = _textTime;
        // Toolbox.Get<PlayerStats>().CoinCount = CoinCount;
    }

    // private void OnDestroy()
    // {
    //    Toolbox.Get<Save>()._save.MoneyCount = _coinCount;
    // }
}