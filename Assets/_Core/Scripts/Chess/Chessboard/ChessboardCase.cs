using UnityEngine;

namespace Chess.Chessboard
{
    public class ChessboardCase : MonoBehaviour
    {
        [SerializeField] private Vector2 _casePosition;
        public Vector2 CasePosition => _casePosition;
        
        private AChessman _currentChessman;

        private void Start()
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, int.MaxValue))
            {
                if (hit.transform.TryGetComponent<AChessman>(out AChessman chessman))
                {
                    SetChessmanInCase(chessman);
                }
            }
        }

        public void SetChessmanInCase(AChessman chessman)
        {
            if (_currentChessman != null)
            {
                Destroy(_currentChessman.gameObject);
            }
            _currentChessman = chessman;
            _currentChessman.SetPositionInChessboard(_casePosition);
            _currentChessman.SetPositionRpc(transform.position + new Vector3(0, 0.5f, 0));
        }
    }
}
