using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces.SoundsTypes;
using UnityEngine;

public class SoundVisitorComponent : MonoBehaviour,ISoundVisitor
{
    [SerializeField] private WeaponData _weaponData;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetWeaponData(WeaponData data)
    {
        _weaponData = data;
    }
    public void Visit(IChestSound sound)
    {
        _audioSource.PlayOneShot(_weaponData.chest);
    }

    public void Visit(IBarrelSound sound)
    {
        _audioSource.PlayOneShot(_weaponData.barrel);
    }
    
    public void Visit(IBodySound sound)
    {
        _audioSource.PlayOneShot(_weaponData.bodySound);
    }

}
