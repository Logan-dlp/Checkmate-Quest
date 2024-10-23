using System;
using Event;
using Event.Listener;
using Multiplayer;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;
    
        [Header("White Player")]
        [SerializeField] private Vector3 _whitePlayerTransform;
        [SerializeField] private LayerMask _whitePlayerCullingMask;
        [SerializeField] private ActionEvent _whitePlayerEvent;
    
        [Header("Black Player")]
        [SerializeField] private Vector3 _blackPlayerTransform;
        [SerializeField] private LayerMask _blackPlayerCullingMask;
        [SerializeField] private ActionEvent _blackPlayerEvent;
    
        public NetworkVariable<bool> _isMyTurn = new NetworkVariable<bool>(false);

        private void Start()
        {
            if (transform.TryGetComponent<ActionEventListener>(out ActionEventListener actionEventListener))
            {
                switch (_personalSocket.PersonalSocketType)
                {
                    case Socket.Host:
                        transform.position = _whitePlayerTransform;
                        transform.GetComponentInChildren<Camera>().cullingMask = _whitePlayerCullingMask;
                        actionEventListener.ActionEvent = _whitePlayerEvent;
                        _isMyTurn.Value = true;
                        break;
                    case Socket.Client:
                        transform.position = _blackPlayerTransform;
                        transform.GetComponentInChildren<Camera>().cullingMask = _blackPlayerCullingMask;
                        actionEventListener.ActionEvent = _blackPlayerEvent;
                        _isMyTurn.Value = false;
                        break;
                }
            }
        }

        public void ToggleTurn()
        {
            _isMyTurn.Value = !_isMyTurn.Value;
        }
        
        public void PlayerClick()
        {
            if (_isMyTurn.Value)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100))
                {
                    Destroy(hit.transform.gameObject);
                    SendTurn();
                }
            }
        }
        
        private void SendTurn()
        {
            _whitePlayerEvent.InvokeEvent();
            _blackPlayerEvent.InvokeEvent();
        }
    }
}
