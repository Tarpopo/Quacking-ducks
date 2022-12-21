using UnityEngine;

public class WeaponItem : SceneItem
{
    public WeaponData WeaponData;

    //public WeaponData WeaponData => _weaponData;
    [HideInInspector] public int CurrentBullet;
    public Animator Animator;
    public Transform ShootTransform;

    public override void OnStart()
    {
        ItemsSpawner = Toolbox.Get<ItemsSpawner>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = transform;
        _baseLayer = gameObject.layer;
        WeaponData = Instantiate(WeaponData ? WeaponData : ItemsSpawner.weaponDataList.Random());
        if (WeaponData.shoot) WeaponData.shoot = Instantiate(WeaponData.shoot);
        data = WeaponData;
        CurrentBullet = WeaponData.bulletCount;
        _spriteRenderer.sprite = WeaponData.fullSprite;
        _collider.size = WeaponData.sizeColider;
        _collider.offset = WeaponData.offsetColider;
        ShootTransform.localPosition = WeaponData.shootPos;
        print(ShootTransform.gameObject);
        WeaponData.shoot.SetShootTransform(ShootTransform);
        _baseSortingLayer = _spriteRenderer.sortingOrder;
        Animator = GetComponentInChildren<Animator>();
    }

    public Animator GetWeaponAnimator()
    {
        if (Animator == null) Animator = GetComponentInChildren<Animator>();
        return Animator;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        Animator.Play(WeaponData.idleAnim.name);
        CurrentBullet = WeaponData.bulletCount;
    }

    public override void TakeItem(Transform parent, Vector3 pos)
    {
        transform.SetParent(parent);
        //_collider.enabled = false;
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void QuitItem(Vector3 dir)
    {
        transform.SetParent(null);
        //Animator.Play(WeaponData.idleAnim.name);
        //_collider.enabled = true;
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.AddForce(-Vector2.right * transform.localScale.x * 0.01f, ForceMode2D.Impulse);
    }


    public override void Destroing()
    {
    }

    public override void ApplyDamage(int damage, Vector2 pos, float force)
    {
        ItemsSpawner.DespawnObject(gameObject);
    }
}