using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    [SerializeField] private Vector3[] _coinPosition;
    private Loader _loader;
    private void Start()
    {
        _loader = Toolbox.Get<Loader>();
    }

    public void DropCoins()
    {
        foreach (var coinPos in _coinPosition)
        {
            _loader.SpawnObject(ObjectId.Coin, true).transform.position = transform.TransformPoint(coinPos);
        }
    }
}
