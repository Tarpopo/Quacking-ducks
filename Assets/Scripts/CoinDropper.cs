using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    [SerializeField] private Vector3[] _coinPosition;
    private ItemsSpawner _itemsSpawner;

    private void Start()
    {
        _itemsSpawner = Toolbox.Get<ItemsSpawner>();
    }

    public void DropCoins()
    {
        foreach (var coinPos in _coinPosition)
        {
            _itemsSpawner.SpawnObject(ObjectId.Coin, true).transform.position = transform.TransformPoint(coinPos);
        }
    }
}