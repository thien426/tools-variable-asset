
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace T70.VariableAsset
{
   [CreateAssetMenu(fileName = "Event Asset", menuName = VariableConst.MenuPath.Event, order = VariableConst.Order.Event)]
    public class EventAsset : ScriptableObject 
    {
        private readonly HashSet<EventAssetListener> listeners = new HashSet<EventAssetListener>(); // nonserialized
        
        public void Raise()
        {
            foreach (var item in listeners)
            {
                item.OnEventRaise();
            }

            evt?.Invoke();
        }

        public void AddListener(EventAssetListener lsn)
        {
            if (listeners.Contains(lsn)) return;
            listeners.Add(lsn);
        }

        public void RemoveListener(EventAssetListener lsn)
        {
            if (!listeners.Contains(lsn)) return;
            listeners.Remove(lsn);
        }

        public UnityEvent evt = new UnityEvent();
        public void Add(UnityAction ac)
        {
            evt.RemoveListener(ac);
            evt.AddListener(ac);
        }
        public void Remove(UnityAction ac)
        {
            evt.RemoveListener(ac);
        }


    }
}