using Unity.Netcode;

namespace Chess.Chessman
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