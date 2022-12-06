using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace.Signals
{
    public class SignalHub
    {
        private readonly Dictionary<Type, ISignal> _signals = new Dictionary<Type, ISignal>();

        public T Get<T>() where T : ISignal, new()
        {
            var signalType = typeof(T);

            if (_signals.TryGetValue(signalType, out var signal))
            {
                return (T)signal;
            }

            return (T)Bind(signalType);
        }

        public void RemoveAllListeners<T>() where T : ISignal
        {
            var signalType = typeof(T);
            if (_signals.TryGetValue(signalType, out var signal) == false) return;
            signal.RemoveAllListeners();
        }

        public void AddListenerToHash(string signalHash, Action handler)
        {
            var signal = GetSignalByHash(signalHash);
            if (signal is Signal signal1)
            {
                signal1.AddListener(handler);
            }
        }

        public void RemoveListenerFromHash(string signalHash, Action handler)
        {
            ISignal signal = GetSignalByHash(signalHash);
            if (signal != null && signal is Signal signal1)
            {
                signal1.RemoveListener(handler);
            }
        }

        private ISignal Bind(Type signalType)
        {
            if (_signals.TryGetValue(signalType, out var signal))
            {
                UnityEngine.Debug.LogError($"Signal already registered for type {signalType.ToString()}");
                return signal;
            }

            signal = (ISignal)Activator.CreateInstance(signalType);
            _signals.Add(signalType, signal);
            return signal;
        }

        private ISignal Bind<T>() where T : ISignal, new()
        {
            return Bind(typeof(T));
        }

        private ISignal GetSignalByHash(string signalHash)
        {
            return _signals.Values.FirstOrDefault(signal => signal.Hash == signalHash);
        }
    }
}