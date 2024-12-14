using Unity.Netcode;
using UnityEngine;

namespace Chess.Chessman
{
    public class Pawn : AChessman
    {
        private Vector2 _positionInChessboard = Vector2.zero;
        
        public override AChessman SelectChessman()
        {
            SetPositionRpc(transform.position + new Vector3(0, .5f, 0));
            return this;
        }

        public override void UnselectChessman()
        {
            SetPositionRpc(transform.position - new Vector3(0, .5f, 0));
        }

        public override void SetPositionInChessboard(Vector2 position)
        {
            _positionInChessboard = position;
        }

        public override Vector2 GetPositionInChessboard()
        {
            return _positionInChessboard;
        }

        [Rpc(SendTo.Everyone)]
        public override void SetPositionRpc(Vector3 position)
        {
            transform.position = position;
        }
    }
}