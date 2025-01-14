using JetBrains.Annotations;
using UnityEngine;

namespace HarryPoter.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _moveTarget;

        [Space]
        [Header("Configs")]
        [SerializeField] private float _minDistanceToAchieveTarget = 0.5f;
        [SerializeField] private float _moveSpeed = 0.08f;

        public bool IsReachedPosition
        {
            get
            {
                return Vector3.Distance(transform.position, _moveTarget.position) < _minDistanceToAchieveTarget;
            }
        }

        public bool IsPositioning { get; private set; }
        

        public void ChangePositioning(bool isPositioning)
        {
            IsPositioning = isPositioning;
        }

        public void UpdateMoveTarget(Vector3 moveTarget)
        {
            _moveTarget.position = new Vector3(moveTarget.x, moveTarget.y, moveTarget.z);
            _moveTarget.localPosition = new Vector3(_moveTarget.localPosition.x, _moveTarget.localPosition.y, _moveTarget.localPosition.z);
        }

        private void FixedUpdate()
        {
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