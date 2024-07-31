using UnityEngine;

namespace HarryPoter.Core
{
    public class ChessBox : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private Animation _animation;
        
        public void Complete()
        {
            _animation.Play();
            _quest.Complete();
        }
    }
}