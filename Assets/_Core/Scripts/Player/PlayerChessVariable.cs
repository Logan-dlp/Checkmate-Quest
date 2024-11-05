using Event;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "new_" + nameof(PlayerChessVariable), menuName = "Scriptable Objects/Player/Chess Variable")]
    public class PlayerChessVariable : ScriptableObject
    {
        public Vector3 _playerPosition;
        public Vector3 _playerRotation;
        public LayerMask _playerCullingMask;
        public LayerMask _playerInteractingMask;
        public ActionEvent _playerEvent;
    }
}