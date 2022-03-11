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

        public Action<Vector3> OnPlayerMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _isMoving = true;
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            var fixedDeltaTime = Time.fixedDeltaTime;
            var verticalMovement = Vector3.forward * playerSettings.verticalSpeed * fixedDeltaTime;
            var horizontalMovement = GetClampedDrag(fixedDeltaTime);
            var moveVector = verticalMovement + horizontalMovement;
            _rb.MovePosition(_rb.position + moveVector);
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("LevelEndTrigger")) return;
            _isMoving = false;
        }

        private void OnEnable()
        {
            EventBus.OnLevelWin += OnLevelWin;
            EventBus.OnLevelLose += OnLevelLose;
            EventBus.OnLevelReset += OnLevelReset;
            EventBus.OnTapToPlay += OnTapToPlay;
        }

        private void OnDisable()
        {
            EventBus.OnLevelWin -= OnLevelWin;
            EventBus.OnLevelLose -= OnLevelLose;
            EventBus.OnLevelReset -= OnLevelReset;
            EventBus.OnTapToPlay += OnTapToPlay;
        }

        private void OnTapToPlay()
        {
        }

        private void OnLevelReset()
        {
        }

        private void OnLevelWin()
        {
        }

        private void OnLevelLose()
        {
        }
    }
}