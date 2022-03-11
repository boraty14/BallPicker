using System;
using UnityEngine;

namespace Core
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        private float _startingDistance;

        private void Start()
        {
            _startingDistance = playerTransform.position.z - transform.position.z;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            currentPosition.z = playerTransform.position.z - _startingDistance;
            transform.position = currentPosition;
        }
    }
}
