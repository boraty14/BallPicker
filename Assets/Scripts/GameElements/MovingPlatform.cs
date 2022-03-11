using System;
using System.Collections;
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

        private void OnEnable()
        {
            EventBus.OnLevelEndTrigger += OnLevelEndTrigger;
        }

        private void OnDisable()
        {
            EventBus.OnLevelEndTrigger -= OnLevelEndTrigger;
        }

        private void OnLevelEndTrigger()
        {
            StartCoroutine(CheckLevelEndStatus());
        }

        private void Start()
        {
            transform.position -= platformSettings.moveHeight * Vector3.up;
        }

        private IEnumerator CheckLevelEndStatus()
        {
            yield return new WaitForSeconds(LevelManager.Instance.CurrentLevelEndWaitDuration);
            if (_hitCollectableCount >= LevelManager.Instance.CurrentLevelNeededObject)
            {
                Debug.Log("win");
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