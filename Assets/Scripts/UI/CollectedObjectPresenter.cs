using System;
using Core;
using GameElements;
using Level;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CollectedObjectPresenter : MonoBehaviour
    {
        [SerializeField] private MovingPlatform movingPlatform;
        private TextMeshProUGUI _objectCountText;
        private int _levelNeededObjectCount;
        
        private void Awake()
        {
            _objectCountText = GetComponent<TextMeshProUGUI>();
        }

        
        private void OnEnable()
        {
            EventBus.OnLevelReset += OnLevelReset;
            EventBus.OnLevelWin += OnLevelWin;
            movingPlatform.OnCollectedObjectIncrease += OnCollectedObjectIncrease;
        }
        
        private void OnDisable()
        {
            EventBus.OnLevelReset -= OnLevelReset;
            EventBus.OnLevelWin -= OnLevelWin;
            movingPlatform.OnCollectedObjectIncrease -= OnCollectedObjectIncrease;
        }

        private void OnLevelReset()
        {
            _levelNeededObjectCount = LevelManager.Instance.CurrentLevelNeededObject;
            UpdateText(0);
        }
        
        private void OnLevelWin()
        {
            _objectCountText.gameObject.SetActive(false);
        }

        private void OnCollectedObjectIncrease(int collectedObjectCount)
        {
            UpdateText(collectedObjectCount);
        }

        private void UpdateText(int collectedObjectCount)
        {
            _objectCountText.text = $"{collectedObjectCount}/{_levelNeededObjectCount}";

        }
    }
}
