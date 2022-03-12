using Core;
using UnityEngine;

namespace Environment
{
    public class Barrier : MonoBehaviour
    {
        private Transform _playerTransform;
        private void OnEnable()
        {
            _playerTransform = FindObjectOfType<Mover>().transform;
            EventBus.OnLevelReset += OnLevelReset;
        }

        private void OnDisable()
        {
            EventBus.OnLevelReset -= OnLevelReset;
        }

        private void OnLevelReset()
        {
            var currentPosition = transform.position;
            currentPosition.z = _playerTransform.position.z;
            transform.position = currentPosition;
        }
    }
}
