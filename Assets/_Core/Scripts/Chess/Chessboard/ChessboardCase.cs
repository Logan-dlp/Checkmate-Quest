using UnityEngine;

namespace Chess.Chessboard
{
    public class ChessboardCase : MonoBehaviour
    {
        [SerializeField] private Vector2 _casePosition;
        public Vector2 CasePosition => _casePosition;
        
        private AChessman _currentChessman;
        public AChessman CurrentChessman => _currentChessman;

        private void Start()
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, int.MaxValue))
            {
                if (hit.transform.TryGetComponent<AChessman>(out AChessman chessman))
                {
                    _currentChessman = chessman;
                    _currentChessman.SetPositionInChessboard(_casePosition);
                    _currentChessman.SetPositionRpc(transform.position + new Vector3(0, .5f, 0));
                }
            }
        }

        public void SetChessmanInCase(AChessman chessman)
        {
            _currentChessman = chessman;
            _currentChessman.SetPositionInChessboard(_casePosition);
            _currentChessman.SetPositionRpc(transform.position + new Vector3(0, 1, 0));
        }
    }
}
