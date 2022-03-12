using Core;
using Level;
using UnityEngine;

namespace Environment
{
    public class Barrier : MonoBehaviour
    {
        //private Transform _playerTransform;
        private void OnEnable()
        {
            //_playerTransform = FindObjectOfType<Mover>().transform;
            EventBus.OnLevelReset += OnLevelReset;
        }

        private void OnDisable()
        {
            EventBus.OnLevelReset -= OnLevelReset;
        }

        private void OnLevelReset()
        {
            var currentPosition = transform.position;
            currentPosition.z = LevelManager.Instance.GetLevelInstantiateOffset();
            transform.position = currentPosition;
        }
    }
}
