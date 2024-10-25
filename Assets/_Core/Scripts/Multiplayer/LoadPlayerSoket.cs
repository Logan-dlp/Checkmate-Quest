using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

namespace Multiplayer
{
    public class LoadPlayerSoket : MonoBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;

        private void Start()
        {
            string ipAdress = _personalSocket.IpAdress;
            
            switch (_personalSocket.PersonalSocketType)
            {
                case Socket.Server:
                    NetworkManager.Singleton.StartServer();
                    break;
                case Socket.Host:
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("0.0.0.0", 7777);
                    NetworkManager.Singleton.StartHost();
                    break;
                case Socket.Client:
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ipAdress, 7777);
                    NetworkManager.Singleton.StartClient();
                    break;
            }
        }
    }
}