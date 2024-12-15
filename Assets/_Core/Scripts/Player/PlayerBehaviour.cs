using Chess;
using Chess.Chessboard;
using Event;
using Event.Listener;
using Multiplayer;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        [SerializeField] private PersonalSocket _personalSocket;
        
        [Header("Debug")]
        [SerializeField] private TextMeshProUGUI _socketText;
        [SerializeField] private TextMeshProUGUI _playerEventText;
        [SerializeField] private TextMeshProUGUI _isMyTurnText;
        
        [SerializeField] private PlayerChessVariable _whitePlayerVariable;
        [SerializeField] private PlayerChessVariable _blackPlayerVariable;

        private Camera _mainCamera;
        private AChessman _currentChessman;
        private LayerMask _currentInteractingMask;
        
        private bool _isMyTurn = false;
        
        private void Start()
        {
            _mainCamera = GetComponentInChildren<Camera>();
            
            if (transform.TryGetComponent<ActionEventListener>(out ActionEventListener actionEventListener))
            {
                switch (_personalSocket.PersonalSocketType)
                {
                    case Socket.Host:
                        transform.position = _whitePlayerVariable._playerPosition;
                        transform.rotation = Quaternion.Euler(_whitePlayerVariable._playerRotation);
                        _mainCamera.cullingMask = _whitePlayerVariable._playerCullingMask;
                        _currentInteractingMask = _whitePlayerVariable._playerInteractingMask;
                        actionEventListener.SetEvent(_whitePlayerVariable._playerEvent);
                        _isMyTurn = true;
                        break;
                    case Socket.Client:
                        transform.position = _blackPlayerVariable._playerPosition;
                        transform.rotation = Quaternion.Euler(_blackPlayerVariable._playerRotation);
                        _mainCamera.cullingMask = _blackPlayerVariable._playerCullingMask;
                        _currentInteractingMask = _blackPlayerVariable._playerInteractingMask;
                        actionEventListener.SetEvent(_blackPlayerVariable._playerEvent);
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
        
        public void OnClick(InputAction.CallbackContext _ctx)
        {
            if (_ctx.started)
            {
                if (_isMyTurn)
                {
                    Vector3 targetDirection = Input.mousePosition + new Vector3(0, 0, 10);
                    targetDirection = _mainCamera.ScreenToWorldPoint(targetDirection);
                    
                    if (Physics.Raycast(transform.position, targetDirection - transform.position, out RaycastHit hit, int.MaxValue))
                    {
                        if (hit.transform.TryGetComponent<AChessman>(out AChessman chessman) && _currentInteractingMask == (_currentInteractingMask | (1 << hit.transform.gameObject.layer)))
                        {
                            if (_currentChessman != null)
                            {
                                if (_currentChessman != chessman)
                                {
                                    _currentChessman.UnselectChessman();
                                    _currentChessman = chessman.SelectChessman();
                                }
                            }
                            else
                            {
                                _currentChessman = chessman.SelectChessman();
                            }
                        }
                        else if (_currentChessman != null)
                        {
                            if (hit.transform.TryGetComponent<ChessboardCase>(out ChessboardCase chessboardCase))
                            {
                                chessboardCase.SetChessmanInCase(_currentChessman);
                                _currentChessman.UnselectChessman();
                                _currentChessman = null;
                                SendTurnRpc();
                            }
                        }
                    }
                    else
                    {
                        if (_currentChessman != null)
                        {
                            _currentChessman.UnselectChessman();
                            _currentChessman = null;
                        }
                    }
                }
            }
        }
        
        [Rpc(SendTo.Everyone)]
        private void SendTurnRpc()
        {
            _whitePlayerVariable._playerEvent.InvokeEvent();
            _blackPlayerVariable._playerEvent.InvokeEvent();
        }
    }
}
