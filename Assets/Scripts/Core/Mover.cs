using System;
using Managers;
using SO;
using UnityEngine;

namespace Core
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private SPlayerSettings playerSettings;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            //_rb.AddForce(Vector3.one,ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
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
                return Vector3.zero;
            }
            return drag * fixedDeltaTime * Vector3.right;
            //var horizontalVelocity = Vector3.right * playerSettings.horizontalSpeed * drag;
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