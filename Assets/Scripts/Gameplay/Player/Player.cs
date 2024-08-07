using UnityEngine;

namespace HarryPoter.Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _centerEye;

        public Transform CenterEye => _centerEye;
    }
}