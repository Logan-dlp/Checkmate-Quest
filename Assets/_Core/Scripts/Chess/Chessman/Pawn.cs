using Unity.Netcode;
using UnityEngine;

namespace Chess.Chessman
{
    public class Pawn : AChessman
    {
        public override AChessman SelectChessman()
        {
            SetPositionRpc(transform.position + new Vector3(0, .5f, 0));
            return this;
        }

        public override void UnselectChessman()
        {
            SetPositionRpc(transform.position - new Vector3(0, .5f, 0));
        }

        [Rpc(SendTo.ClientsAndHost)]
        protected override void SetPositionRpc(Vector3 position)
        {
            transform.position = position;
        }
    }
}