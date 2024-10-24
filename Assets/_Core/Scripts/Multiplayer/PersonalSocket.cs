using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(fileName = "new_" + nameof(PersonalSocket), menuName = "Network/Personal Socket")]
    public class PersonalSocket : ScriptableObject
    {
        [SerializeField] private Socket _personalSocketType;
        public Socket PersonalSocketType => _personalSocketType;

        public void ChangeSocket(int index)
        {
            _personalSocketType = (Socket)index;
        }
    }
}