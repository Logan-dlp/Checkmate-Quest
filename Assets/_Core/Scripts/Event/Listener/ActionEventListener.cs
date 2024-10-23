using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Event.Listener
{
    public class ActionEventListener : NetworkBehaviour
    {
        [SerializeField] private ActionEvent _actionEvent;
        public ActionEvent ActionEvent
        {
            get => _actionEvent;
            set => _actionEvent = value;
        }
        
        [SerializeField] private UnityEvent _callbacks;

        private void InvokeEvent()
        {
            _callbacks?.Invoke();
        }
    }
}