using Unity.Netcode;
using UnityEngine;

namespace Chess
{
    public abstract class IChessman : NetworkBehaviour
    {
        public abstract IChessman SelectChessman();
        public abstract void UnselectChessman();
        protected abstract void SetPositionRpc(Vector3 position);
    }
}