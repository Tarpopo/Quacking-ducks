using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunksSpawner : MonoBehaviour, ITick
{
    public GameObject chunkBottom;
    public List<Items> chunks;
    public GameObject enemyPrefab;

    private List<int> _freeChunks;
    private List<int> _occupiedChunks;
    private Transform _playerTransform;
    private float _lastYPos;
    private ItemsSpawner _itemsSpawner;


    private const float StepY = 1.56f * 25;

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _lastYPos = 0;
        _freeChunks = new List<int>(chunks.Count);
        _occupiedChunks = new List<int>(chunks.Count);
        _itemsSpawner = Toolbox.Get<ItemsSpawner>();
        ManagerUpdate.AddTo(this);

        for (var index = 0; index < chunks.Count; index++)
        {
            _freeChunks.Add(index);
            var chunk = chunks[index];
            var allTransform = chunk.Chunk.GetComponentsInChildren<Transform>();
            foreach (var all in allTransform)
            {
                if (all.CompareTag("Tree")) chunk.Trees.Add(all);
                else if (all.CompareTag("Box")) chunk.Box.Add(all);
                else if (all.CompareTag("Enemy")) chunk.enemies.Add(all);
            }

            chunk.Chunk.SetActive(false);
        }

        for (var i = 0; i < 2; i++)
        {
            SpawnRandChunk();
        }

        print(_freeChunks.Random());
    }

    private void SpawnRandChunk()
    {
        var rand = Random.Range(0, _freeChunks.Count);
        _occupiedChunks.Add(_freeChunks[rand]);
        chunks[_freeChunks[rand]].Chunk.transform.position = new Vector2(0, _lastYPos + StepY);
        SpawnEnemy(_freeChunks[rand]);
        _lastYPos += StepY;
        chunks[_freeChunks[rand]].Chunk.SetActive(true);
        _freeChunks.RemoveAt(rand);
    }

    private void SpawnEnemy(int chunkIndex)
    {
        foreach (var enemy in chunks[chunkIndex].enemies)
        {
            _itemsSpawner.SpawnObject(ObjectId.Enemy, true).transform.position = enemy.position;
        }
    }

    private void FreeOccupiedChunks()
    {
        _freeChunks.Add(_occupiedChunks[0]);
        chunks[_occupiedChunks[0]].Chunk.SetActive(false);
        _occupiedChunks.RemoveAt(0);
    }


    public void Tick()
    {
        if (_playerTransform.position.y > _lastYPos + 20)
        {
            SpawnRandChunk();
            FreeOccupiedChunks();
            //_lastYPos += StepY;
        }
    }
}

[Serializable]
public struct Items
{
    public GameObject Chunk;
    public List<Transform> Trees;
    public List<Transform> Box;
    public List<Transform> enemies;
}