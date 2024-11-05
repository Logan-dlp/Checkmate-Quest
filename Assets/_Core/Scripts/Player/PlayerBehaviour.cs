using Chess;
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
    
        [Header("White Player")]
        [SerializeField] private Vector3 _whitePlayerTransform;
        [SerializeField] private LayerMask _whitePlayerCullingMask;
        [SerializeField] private LayerMask _whitePlayerInteractingMask;
        [SerializeField] private ActionEvent _whitePlayerEvent;
    
        [Header("Black Player")]
        [SerializeField] private Vector3 _blackPlayerTransform;
        [SerializeField] private LayerMask _blackPlayerCullingMask;
        [SerializeField] private LayerMask _blackPlayerInteractingMask;
        [SerializeField] private ActionEvent _blackPlayerEvent;

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
                        SetPositionRpc(_whitePlayerTransform);
                        _mainCamera.cullingMask = _whitePlayerCullingMask;
                        _currentInteractingMask = _whitePlayerInteractingMask;
                        actionEventListener.SetEvent(_whitePlayerEvent);
                        _isMyTurn = true;
                        break;
                    case Socket.Client:
                        SetPositionRpc(_blackPlayerTransform);
                        _mainCamera.cullingMask = _blackPlayerCullingMask;
                        _currentInteractingMask = _blackPlayerInteractingMask;
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

        [Rpc(SendTo.ClientsAndHost)]
        private void SetPositionRpc(Vector3 position)
        {
            transform.position = position;
        }
        
        [Rpc(SendTo.ClientsAndHost)]
        private void SendTurnRpc()
        {
            _whitePlayerEvent.InvokeEvent();
            _blackPlayerEvent.InvokeEvent();
        }
    }
}
