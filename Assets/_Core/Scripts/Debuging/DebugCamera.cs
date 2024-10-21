using Multiplayer;
using Unity.Netcode;
using UnityEngine;

namespace Debuging
{
    public class DebugCamera : NetworkBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _player1;
        [SerializeField] private LayerMask _player2;
        
        public override void OnNetworkSpawn()
        {
            switch (_personalSocket.PersonalSocketType)
            {
                case Socket.Host:
                    _camera.cullingMask = _player1;
                    break;
                case Socket.Client:
                    _camera.cullingMask = _player2;
                    break;
            }
        }
    }
}