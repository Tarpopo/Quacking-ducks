using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ItemsSpawner : ManagerBase, IAwake, ISceneChanged
{
    private Animator particleAnim;
    public List<SpawnedObjects> objects;
    public Ducks currentDuck;
    public List<DataActor> Ducks;
    public List<WeaponData> weaponDataList = new List<WeaponData>();

    public Dictionary<GameObject, SceneItem> Items = new Dictionary<GameObject, SceneItem>();
    // public Dictionary<GameObject, IDamagable> damagableObjects = new Dictionary<GameObject, IDamagable>();

    //public Vector3[] TransformsChest;
    private ManagerPool _managerPool;
    private int _currentWeaponCount;
    public Transform poolTransform;

    public override void ClearScene()
    {
        // Items.Clear();
        // damagableObjects.Clear();
    }

    public void OnChangeScene()
    {
        // if (Toolbox.Get<SceneController>().GetIsMainScene()) return;
        // Debug.Log("WTF");
        // SetDataDuck();
        //
        // for (int i = 0; i < objects.Count; i++)
        // {
        //     var dictId = objects[i].dictId;
        //     if (dictId == DictId.Damagable)
        //     {
        //         _managerPool.AddPool(PoolType.Entities)
        //             .PopulateWith(objects[i].prefab, objects[i].countPref, damagableObjects);
        //     }
        //     else if (dictId == DictId.Item)
        //     {
        //         _managerPool.AddPool(PoolType.Entities).PopulateWith(objects[i].prefab, objects[i].countPref, Items);
        //         //_managerPool.AddPool(PoolType.Entities).PopulateWith(objects[i].prefab,objects[i].countPref, damagableObjects);
        //     }
        //     else if (dictId == DictId.Particle)
        //     {
        //         continue;
        //     }
        //     else
        //     {
        //         _managerPool.AddPool(PoolType.Entities).PopulateWith(objects[i].prefab, objects[i].countPref);
        //     }
        // }
        //
        // foreach (var variable in Items)
        // {
        //     damagableObjects.Add(variable.Key, variable.Value);
        // }
    }

    public void OnAwake()
    {
        _managerPool = Toolbox.Get<ManagerPool>();
        // _managerPool = Toolbox.Get<ManagerPool>();

        // _managerPool.AddPool(PoolType.Entities).PopulateWith(barrel,6, damagableDict);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(tree,2, damagableDict);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(weaponChest,6, damagableDict);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(weapon,6, damagableDict);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(particle, 6,  particDictionary);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(bullet,10);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(root,2);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(rocketBullet, 4);
        // _managerPool.AddPool(PoolType.Entities).PopulateWith(enemy, 8,damagableObjects);

        // for (int i = 0; i < TransformsChest.Length; i++)
        // {
        //     SpawnObject(TransformsChest[i], ObjectId.WeaponChest, true);
        // }
    }
    // public void Despawn(GameObject obj)
    // {
    //     _currentWeaponCount--;
    //     _managerPool.Despawn(PoolType.Entities,obj);
    // }
    // public void Spawn()
    // {
    //     if(_currentWeaponCount<6)
    //     {
    //         // _managerPool.Spawn(PoolType.Entities, weaponChest);
    //         // _managerPool.Spawn(PoolType.Entities, barrel,new Vector3(-0.24f,-0.27f,0));
    //         _currentWeaponCount++;
    //     }
    // }

    // public void DespawnParticles(GameObject obj)
    // {
    //     _managerPool.Despawn(PoolType.Entities,obj);
    // }

    // public Animator SpawnParticles(Vector3 position)
    // {
    //     var go =_managerPool.Spawn(PoolType.Entities,objects[(int)ObjectId.Particle].prefab);
    //     go.transform.position = position;
    //     particDictionary.TryGetValue(go,out particleAnim);
    //     return particleAnim;
    // }
    // public GameObject SpawnBullet(GameObject go,bool setActive)
    // {
    //     return _managerPool.Spawn(PoolType.Entities, go,setActive:setActive);
    // }
    public void SetDataDuck()
    {
        GameObject.FindWithTag("Player").GetComponent<Actor>().data = Ducks[(int)currentDuck];
    }

    public GameObject SpawnObject(Vector3 position, ObjectId id, bool setActive, Transform parent = null)
    {
        return _managerPool.Spawn(PoolType.Entities, objects[(int)id].prefab, position, setActive: setActive,
            parent: parent);
    }

    public GameObject SpawnObject(ObjectId id, bool setActive)
    {
        return _managerPool.Spawn(PoolType.Entities, objects[(int)id].prefab, setActive: setActive);
    }

    public void DespawnObject(GameObject obj)
    {
        _managerPool.Despawn(PoolType.Entities, obj);
    }
}

[Serializable]
public struct SpawnedObjects
{
    public DictId dictId;
    public GameObject prefab;
    public int countPref;
}

public enum Ducks
{
    Rambo,
    Aqua,
    Smoking,
    Ninja
}

public enum ObjectId
{
    //Barrel,
    Weapon,
    Particle,
    Tree,
    Root,
    Enemy,

    //WeaponChest, 
    RocketBullet,
    Bullet,
    Coin,
    RocketParticle,
    ShutGunParticle,
    shutGunBullet,
}

public enum DictId
{
    Particle,
    Damagable,
    Item,
    Empty,
}