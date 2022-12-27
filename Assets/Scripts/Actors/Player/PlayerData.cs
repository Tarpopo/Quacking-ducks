using System;
using UnityEngine;

[Serializable]
public class PlayerData : BaseActorData
{
    public float JumpForce => _jumpForce;
    [SerializeField] private float _jumpForce;
}