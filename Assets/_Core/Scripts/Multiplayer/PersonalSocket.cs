using System;
using TMPro;
using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(fileName = "new_" + nameof(PersonalSocket), menuName = "Network/Personal Socket")]
    public class PersonalSocket : ScriptableObject
    {
        [SerializeField] private Socket _personalSocketType;
        public Socket PersonalSocketType => _personalSocketType;

        [SerializeField] private string _ipTarget;
        public string IpTarget => _ipTarget;

        private void OnEnable()
        {
            _ipTarget = "0.0.0.0";
        }

        public void ChangeIpTarget(TextMeshProUGUI ipTarget)
        {
            _ipTarget = ipTarget.text;
        }

        public void ChangeSocket(int index)
        {
            _personalSocketType = (Socket)index;
        }
    }
}