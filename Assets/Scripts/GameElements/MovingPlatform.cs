using System;
using Core;
using DG.Tweening;
using GameSettings;
using Level;
using UnityEngine;

namespace GameElements
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private SMovingPlatformSettings platformSettings;

        private int _hitCollectableCount = 0;

        public Action<int> OnCollectedObjectIncrease;

        public void CheckLevelEndStatus()
        {
            if (_hitCollectableCount >= LevelManager.Instance.CurrentLevelNeededObject)
            {
                EventBus.OnLevelWin?.Invoke();
            }
            else
            {
                EventBus.OnLevelLose.Invoke();
            }
        }

        public void MovePlatform()
        {
            transform.DOMove(transform.position + Vector3.up * platformSettings.moveHeight,
                platformSettings.moveDuration).SetEase(platformSettings.moveEase).OnComplete(() =>
            {

            });
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Collectable")) return;
            if (other.gameObject.GetComponent<Collectable>().HitMovingPlatform())
            {
                _hitCollectableCount++;
                OnCollectedObjectIncrease?.Invoke(_hitCollectableCount);
            }
            
        }
    }
}