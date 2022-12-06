using System;
using UnityEngine;

namespace Triggers
{
    [Serializable]
    public class InterfaceTriggerChecker<T> : BaseTriggerChecker
    {
        public T Interface => _interface;
        private T _interface;

        protected override bool IsThisObject(GameObject gameObject) => gameObject.TryGetComponent(out _interface);
    }
}