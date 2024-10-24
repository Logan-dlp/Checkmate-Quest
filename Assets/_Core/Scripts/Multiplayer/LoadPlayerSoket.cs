using UnityEngine;
using Unity.Netcode;

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
                    NetworkManager.Singleton.StartClient();
                    break;
            }
        }
    }
}