using Unity.Netcode;
using UnityEngine;

namespace Chess
{
    public class Pawn : NetworkBehaviour, IChessman
    {
        [Rpc(SendTo.ClientsAndHost)]
        public void MoveChessRpc()
        {
            transform.position += transform.right;
        }
    }
}