using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Event.Listener
{
    public class ActionEventListener : NetworkBehaviour
    {
        [SerializeField] private ActionEvent _actionEvent;
        public ActionEvent ActionEvent => _actionEvent;
        
        [SerializeField] private UnityEvent _callbacks;

        public void SetEvent(ActionEvent actionEvent)
        {
            _actionEvent = actionEvent;
            _actionEvent.Action += InvokeEvent;
        }

        private void InvokeEvent()
        {
            _callbacks?.Invoke();
        }
    }
}