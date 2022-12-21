using Interfaces.SoundsTypes;
using UnityEngine;

public class Chest : SceneItem, IChestSound
{
    //public float force;

    //public GameObject item;
    // private string hui = "pias";
    //private Animator _particAnimator;
    private ParticleManager _particleManager;

    private void Start()
    {
        _particleManager = Toolbox.Get<ParticleManager>();
        _audioSource = GetComponent<AudioSource>();
        base.OnStart();
        Health = data.health;
        _spriteRenderer.sprite = data.fullSprite;
        // ItemsSpawner.damagableObjects.Add(gameObject,this);
        ItemsSpawner.Items.Add(gameObject, this);
    }

    public override void Destroing()
    {
        // _particAnimator = _loader.SpawnParticles(transform.position);
        // _particAnimator.Play(data.destroyAnim.name);
        //base.Destroing();
        _audioSource.PlaySound(data.destroy);
        _rigidBody.gravityScale = 0;
        _particleManager.PlayParticle(data.destroyAnim, transform.position);
        _rigidBody.Sleep();
        _collider.enabled = false;
        _spriteRenderer.sprite = null;
        Invoke(nameof(DeactivateObject), 0.5f);
        //_loader.DespawnObject(gameObject);
        //Invoke("DistructWithTime",0.5f);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    // public override void DistructWithTime()
    // {
    //     _loader.DespawnObject(gameObject);
    //     _loader.DespawnObject(_particAnimator.gameObject);
    // }
    public override void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius)
    {
        base.ApplyExplosionDamage(damage, pos, force, damageRadius);
        Health = 0;
        Destroing();
        var weapon = ItemsSpawner.SpawnObject(ObjectId.Weapon, true);
        weapon.transform.position = transform.position;
    }

    public override void ApplyDamage(int damage, Vector2 pos, float force)
    {
        if (Health <= 0) return;
        if (damage >= 100) Health = 0;
        Health--;
        if (Health <= 0)
        {
            Destroing();
            print(gameObject + "createWeapon");
            var weapon = ItemsSpawner.SpawnObject(ObjectId.Weapon, true);
            weapon.transform.position = new Vector3(_transform.position.x, _transform.position.y + 0.02f, 0);
            return;
        }

        if (Health == 1)
        {
            _audioSource.PlaySound(data.full);
            _rigidBody.AddForce(((Vector2)_transform.position - pos) * force, ForceMode2D.Impulse);
            //_rigidBody.AddTorque(0.1f,ForceMode2D.Impulse);
            _spriteRenderer.sprite = data.halfSprite;
        }
    }

    public override void PlayDamageSound(ISoundVisitor visitor)
    {
        visitor.Visit(this);
    }
}