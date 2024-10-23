using System;
using UnityEngine;

namespace Event
{
    [CreateAssetMenu(fileName = "new_" + nameof(ActionEvent), menuName = "Event/Action")]
    public class ActionEvent : ScriptableObject
    {
        public Action Action;

        public void InvokeEvent()
        {
            Action?.Invoke();
        }
    }
}