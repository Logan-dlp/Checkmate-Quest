using Unity.Netcode;
using UnityEngine;

namespace Chess
{
    public interface IChessman
    {
        public void MoveChessRpc();
    }
}