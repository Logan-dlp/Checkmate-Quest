using UnityEngine;
using Unity.Netcode;

namespace Multiplayer
{
    public class LoadPlayerSoket : MonoBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;
    
        private static NetworkManager _networkManager;

        private void Awake()
        {
            _networkManager = FindObjectOfType<NetworkManager>();
        }

        private void OnEnable()
        {
            switch (_personalSocket.PersonalSocketType)
            {
                case Socket.Server:
                    _networkManager.StartServer();
                    break;
                case Socket.Host:
                    _networkManager.StartHost();
                    break;
                case Socket.Client:
                    _networkManager.StartClient();
                    break;
            }
        }
    }
}