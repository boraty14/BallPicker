using System;
using GameSettings;
using Managers;
using UnityEngine;

namespace Core
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private SPlayerSettings playerSettings;
        private Rigidbody _rb;
        private bool _isMoving = false;

        public Action<float> OnPlayerMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            var fixedDeltaTime = Time.fixedDeltaTime;
            var verticalMovement = Vector3.forward * playerSettings.verticalSpeed * fixedDeltaTime;
            var horizontalMovement = GetClampedDrag(fixedDeltaTime);
            var moveVector = verticalMovement + horizontalMovement;
            _rb.MovePosition(_rb.position + moveVector);
            OnPlayerMove?.Invoke(_rb.position.z);
        }

        private Vector3 GetClampedDrag(float fixedDeltaTime)
        {
            var drag = InputHandler.Instance.GetCurrentDrag();
            var currentPosition = _rb.position;
            if (Mathf.Abs(currentPosition.x + drag * fixedDeltaTime) > playerSettings.horizontalLimit)
            {
                return Mathf.Sign(currentPosition.x) *
                       (playerSettings.horizontalLimit - 0.01f - Mathf.Abs(currentPosition.x)) * Vector3.right;
            }
            return drag * fixedDeltaTime * Vector3.right * playerSettings.horizontalSpeed;
        }

        private void OnEnable()
        {
            EventBus.OnTapToPlay += OnTapToPlay;
            EventBus.OnLevelEndTrigger += OnLevelEndTrigger;
        }

        private void OnDisable()
        {
            EventBus.OnTapToPlay -= OnTapToPlay;
            EventBus.OnLevelEndTrigger -= OnLevelEndTrigger;
        }

        private void OnLevelEndTrigger()
        {
            Debug.Log("dur");
            _isMoving = false;
        }

        private void OnTapToPlay()
        {
            _isMoving = true;
        }
    }
}