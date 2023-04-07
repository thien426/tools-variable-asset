using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace T70.VariableAsset
{
    public class EventAssetListener : MonoBehaviour
    {
        public EventAsset eventAsset;
        public UnityEvent unityEvent;

        public void OnEnable()
        {
            if (eventAsset == null) 
            {
                Debug.LogWarning("eventAsset should not be null !");
                return;
            }

            eventAsset.AddListener(this);
        }

        public void OnDisable()
        {
            if (eventAsset == null) return;
            eventAsset.RemoveListener(this);
        }

        public void OnEventRaise()
        {
            if (unityEvent == null)
            {
                Debug.LogWarning("unityEvent should not be null ! Event raised: " + eventAsset);
                return;
            }

            unityEvent.Invoke();
        }
    }
}