using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class TagTriggerCollector : TriggerCollector<GameObject>
{
    [SerializeField, Tag] private string _tag;
    protected override bool IsThisObject(GameObject gameObject) => gameObject.tag.Equals(_tag);
    protected override GameObject GetComponent(GameObject gameObject) => gameObject;
}