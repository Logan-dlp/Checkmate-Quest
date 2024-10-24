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
            switch (_personalSocket.PersonalSocketType)
            {
                case Socket.Server:
                    NetworkManager.Singleton.StartServer();
                    break;
                case Socket.Host:
                    NetworkManager.Singleton.StartHost();
                    break;
                case Socket.Client:
                    NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData("192.168.1.88", 7777);
                    NetworkManager.Singleton.StartClient();
                    break;
            }
        }
    }
}