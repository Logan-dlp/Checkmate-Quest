using Unity.Netcode;
using UnityEngine;

namespace Chess.Chessman
{
    public class Pawn : NetworkBehaviour, IChessman
    {
        public IChessman SelectChessman()
        {
            Debug.Log("Tu m'as sélectionner !");
            return this;
        }

        [Rpc(SendTo.ClientsAndHost)]
        public void MoveChessRpc()
        {
            transform.position += transform.right;
        }
    }
}