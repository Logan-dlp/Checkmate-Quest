using Unity.Netcode;
using UnityEngine;

namespace Chess
{
    public abstract class AChessman : NetworkBehaviour
    {
        public abstract AChessman SelectChessman();
        public abstract void UnselectChessman();
        protected abstract void SetPositionRpc(Vector3 position);
    }
}