using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopCell : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private AnimationClip plate;
    [SerializeField] private AnimationClip unlock;
    [SerializeField] private AnimationClip _lock;
    [SerializeField] private AnimationClip _lockFalling;
    private BoxCollider2D _boxCollider2D;
    public GameObject Gate;
    public GameObject _lockObj;
    [SerializeField] private Ducks duck;
    private Animator _animator;
    public bool IsActive;
    public int CellIndex;
    public int Price;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void ActivateCell()
    {
        if (IsActive == false)
        {
            var stats = Toolbox.Get<PlayerStats>();
            if (stats.CoinCount - Price < 0) return;
            stats.CoinCount -= Price;
            _boxCollider2D.enabled = false;
            IsActive = true;
            Toolbox.Get<Shop>().ActiveShopItem(CellIndex);
            _animator.Play(unlock.name);
        }
        else
        {
            Toolbox.Get<Loader>().currentDuck=duck;
            print("its a duck");
        }
    }

    public void ActivateBoxCollider()
    {
        _boxCollider2D.enabled = true;
    }

    public void AnimateLockFalling()
    {
        _animator.Play(_lockFalling.name);
    }

    public void AnimateLock()
    {
        Gate.SetActive(false);
        _animator.Play(_lock.name);
    }

    public void AnimatePlate()
    {
        _animator.Play(plate.name);
    }

    public void DeactivateLock()
    {
        _lockObj.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateCell();
    }
}
