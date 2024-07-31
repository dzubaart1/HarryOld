using UnityEngine;

namespace HarryPoter.Core
{
    public class ChessTable : MonoBehaviour
    {
        [SerializeField] private ChessBox _chessBox;
        
        private const string ROOK_TAG = "Rook";
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ROOK_TAG))
            {
                return;
            }
            
            _chessBox.Complete();
        }
    }
}