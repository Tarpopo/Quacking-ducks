using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class TagTriggerChecker : BaseTriggerChecker
{
    [SerializeField, Tag] private string _tag;
    protected override bool IsThisObject(GameObject gameObject) => gameObject.tag.Equals(_tag);
}