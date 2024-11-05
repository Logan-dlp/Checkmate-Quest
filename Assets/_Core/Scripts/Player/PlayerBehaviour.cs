using System;
using Chess;
using Event;
using Event.Listener;
using Multiplayer;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;
        [SerializeField] private TextMeshProUGUI _socketText;
        [SerializeField] private TextMeshProUGUI _playerEventText;
        [SerializeField] private TextMeshProUGUI _isMyTurnText;
    
        [Header("White Player")]
        [SerializeField] private Vector3 _whitePlayerTransform;
        [SerializeField] private LayerMask _whitePlayerCullingMask;
        [SerializeField] private ActionEvent _whitePlayerEvent;
    
        [Header("Black Player")]
        [SerializeField] private Vector3 _blackPlayerTransform;
        [SerializeField] private LayerMask _blackPlayerCullingMask;
        [SerializeField] private ActionEvent _blackPlayerEvent;

        private bool _isMyTurn = false;

        private void Start()
        {
            if (transform.TryGetComponent<ActionEventListener>(out ActionEventListener actionEventListener))
            {
                switch (_personalSocket.PersonalSocketType)
                {
                    case Socket.Host:
                        transform.position = _whitePlayerTransform;
                        transform.GetComponentInChildren<Camera>().cullingMask = _whitePlayerCullingMask;
                        actionEventListener.SetEvent(_whitePlayerEvent);
                        _isMyTurn = true;
                        break;
                    case Socket.Client:
                        transform.position = _blackPlayerTransform;
                        transform.GetComponentInChildren<Camera>().cullingMask = _blackPlayerCullingMask;
                        actionEventListener.SetEvent(_blackPlayerEvent);
                        _isMyTurn = false;
                        break;
                }

                _socketText.text = _personalSocket.PersonalSocketType.ToString();
                _playerEventText.text = actionEventListener.ActionEvent.ToString();
                _isMyTurnText.text = _isMyTurn.ToString();
            }
        }

        public void ToggleTurn()
        {
            _isMyTurn = !_isMyTurn;
            _isMyTurnText.text = _isMyTurn.ToString();
        }
        
        public void PlayerClick()
        {
            if (_isMyTurn)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100))
                {
                    if (hit.transform.TryGetComponent<IChessman>(out IChessman chessman))
                    {
                        chessman.MoveChessRpc();
                        SendTurnRpc();
                    }
                }
            }
        }
        
        [Rpc(SendTo.ClientsAndHost)]
        private void SendTurnRpc()
        {
            _whitePlayerEvent.InvokeEvent();
            _blackPlayerEvent.InvokeEvent();
        }
    }
}
