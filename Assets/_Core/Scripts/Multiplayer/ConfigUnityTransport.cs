using TMPro;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Multiplayer
{
    public class ConfigUnityTransport : MonoBehaviour
    {
        private UnityTransport _unityTransport;

        private void Awake()
        {
            _unityTransport = GetComponent<UnityTransport>();
        }

        public void SetIPAdress(string ipAdress)
        {
            _unityTransport.SetConnectionData(ipAdress, 7777);
        }

        public void SetIPAdress(TextMeshProUGUI ipAdressText)
        {
            string ipAdress = ipAdressText.text;
            _unityTransport.SetConnectionData(ipAdress, 7777);
        }
    }
}