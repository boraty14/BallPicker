using System;
using System.Collections;
using Core;
using UnityEngine;

namespace Level
{
    public class LevelEndTrigger : MonoBehaviour
    {
        private bool _isTriggered = false;

        private void OnEnable()
        {
            EventBus.OnLevelWin += OnLevelWin;
        }

        private void OnDisable()
        {
            EventBus.OnLevelWin -= OnLevelWin;
        }

        private void OnLevelWin()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || _isTriggered) return;
            _isTriggered = true;
            EventBus.OnLevelEndTrigger?.Invoke();
        }
    }
}
