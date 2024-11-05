using Unity.Netcode;
using UnityEngine;

namespace Chess
{
    public abstract class AChessman : NetworkBehaviour
    {
        public abstract AChessman SelectChessman();
        public abstract void UnselectChessman();

        public abstract void SetPositionInChessboard(Vector2 position);
        public abstract Vector2 GetPositionInChessboard();
        
        public abstract void SetPositionRpc(Vector3 position);
    }
}