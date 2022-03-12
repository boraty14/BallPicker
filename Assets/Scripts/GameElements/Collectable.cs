using System;
using Core;
using DG.Tweening;
using GameSettings;
using UnityEngine;

namespace GameElements
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private SCollectableSettings collectableSettings;
        private bool _didHitPlatform = false;

        private void OnEnable()
        {
            EventBus.OnLevelWin += OnLevelWin;
            EventBus.OnLevelLose += OnLevelLose;
        }

        private void OnDisable()
        {
            EventBus.OnLevelWin -= OnLevelWin;
            EventBus.OnLevelLose -= OnLevelLose;
        }

        private void OnLevelWin()
        {
            transform.DOKill();
            Destroy(gameObject);
        }

        private void OnLevelLose()
        {
            transform.DOKill();
            Destroy(gameObject);
        }

        public bool HitMovingPlatform()
        {
            if (_didHitPlatform) return false;
            _didHitPlatform = true;
            transform.DOScale(collectableSettings.destroyScale, collectableSettings.destroyDuration)
                .SetEase(collectableSettings.destroyEase).OnComplete(
                    () =>
                    { 
                        Destroy(gameObject);
                    });
            return true;
        }
    }
}
