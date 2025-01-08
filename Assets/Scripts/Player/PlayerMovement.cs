using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Rigidbody _rigidbody;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _minDistanceToAchieveTarget = 0.5f;
        [SerializeField] private float _moveSpeed = 0.08f;

        public bool IsReachedPosition
        {
            get
            {
                if (_moveTarget == null)
                {
                    return false;
                }
                
                return Vector3.Distance(transform.position, _moveTarget.position) < _minDistanceToAchieveTarget;
            }
        }

        public bool IsPositioning { get; private set; }

        [CanBeNull] private Transform _moveTarget;
        
        private void Awake()
        {
            _moveTarget = new GameObject("MoveTarget").transform;
            _moveTarget.SetParent(transform);
        }

        public void ChangePositioning(bool isPositioning)
        {
            IsPositioning = isPositioning;
        }

        public void UpdateMoveTarget(Vector3 moveTarget)
        {
            if (_moveTarget == null)
            {
                return;
            }
            
            _moveTarget.position = moveTarget;
        }

        private void FixedUpdate()
        {
            if (_moveTarget == null)
            {
                return;
            }
            
            if (IsPositioning)
            {
                if (!IsReachedPosition)
                {
                    SetPosition(_moveTarget.position, _moveSpeed * Time.fixedDeltaTime);   
                }
            }
        }
        
        private void SetPosition(Vector3 moveTargetPos, float speed)
        {
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, moveTargetPos, speed);
        }
    }
}