using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : SceneItem
{
    public float FlyForce;

    public AnimationClip Idle;
    public AnimationClip NoneSprite;

    private Animator _animator;
    // private readonly List<Vector2> _vectors=new List<Vector2>(5)
    // {
    //     Vector2.up,
    //     Vector2.left,
    //     Vector2.right,
    //     new Vector2(0.5f,0.5f),
    //     new Vector2(-0.5f,0.5f),
    // };
    public override void OnStart()
    {
        base.OnStart();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void OnSpawn()
    {
        _rigidBody.gravityScale = 1;
        _collider.enabled = true;
        _animator.Play(Idle.name);
        //_rigidBody.AddForce(_vectors.Random()*FlyForce,ForceMode2D.Impulse);
        Health = data.health;
        _spriteRenderer.sprite = data.fullSprite;
    }
    public override void ApplyDamage(int damage, Vector2 pos, float force)
    {
        Destroing();
    }
    public override void TakeItem(Transform parent, Vector3 pos)
    {
        //_rigidBody.gravityScale=0;
        //_rigidBody.bodyType = RigidbodyType2D.Kinematic;
        //gameObject.SetActive(false);
        //_rigidBody.gravityScale = 0;
        _animator.Play(NoneSprite.name);
        _collider.enabled = false;
        _spriteRenderer.sprite = null;
        Destroing();
        //Invoke(nameof(Destroing),0.5f);
    }
    public override void QuitItem(Vector3 dir){}
}
