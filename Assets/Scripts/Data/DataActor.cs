using System;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "DataActor", menuName = "Data/DataActor")]
public class DataActor : ScriptableObject, IEnum
{
    public Enum EnumValue => _ducks;
    [SerializeField] private Ducks _ducks;
    [Header("Attack Zone")] public Vector3 attackSize;

    public LayerMask whoisEnemy;

    [Header("Animations")] public AnimationClip run;
    public AnimationClip idle;
    public AnimationClip quack;
    public AnimationClip jumpDown;
    public AnimationClip death;
    public AnimationClip takeDamage;
    public AnimationClip deathParticles;
    public AnimationClip ActorLight;

    public RuntimeAnimatorController anim_controller;

    [Header("Health")] public int _health;
    public int curr_health;

    [Header("ActorColider")] public Vector2 colOffset;
    public Vector2 colSize;

    [Header("Air Zone")] public Vector2 sizeCube;
    public Vector2 posCube;

    [Header("Sounds")] public SimpleSound[] stepSounds;
    public SimpleSound quackSound;

    // public SimpleSound stepSound;


    private void OnDisable()
    {
        curr_health = _health;
    }
}