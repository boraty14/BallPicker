using Core;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InputHandler : MonoBehaviourSingleton<InputHandler>
    {

        private int _pointerCount = 0;
        private bool _didTapToPlay = false;
        private float _currentDrag = 0f;

        private void Awake()
        {
            _pointerCount = 0;
        }

        private void OnEnable()
        {
            EventBus.OnLevelReset += OnLevelReset;
            EventBus.OnLevelEndTrigger += OnLevelEndTrigger;
        }

        private void OnDisable()
        {
            EventBus.OnLevelReset -= OnLevelReset;
            EventBus.OnLevelEndTrigger -= OnLevelEndTrigger;
            
        }

        private void OnPointerDown()
        {
            _pointerCount++;
            if(_pointerCount > 1) return;
            if (!_didTapToPlay)
            {
                EventBus.OnTapToPlay?.Invoke();
                _didTapToPlay = true;
            }
        }
        private void OnPointerUp()
        {
            _pointerCount--;
            if (_pointerCount > 0) return;
        }
        
        private void OnDragDelta(Vector2 delta)
        {
            _currentDrag += delta.x;
        }

        public float GetCurrentDrag()
        {
            var returnDrag = _currentDrag;
            _currentDrag = 0f;
            return returnDrag;
        }

        private void OnLevelReset()
        {
            AddListeners();
            _didTapToPlay = false;
        }
        
        private void OnLevelEndTrigger()
        {
            RemoveListeners();
        }
        
        private void AddListeners()
        {
            InputPanel.Instance.OnPointerDownEvent.AddListener(OnPointerDown);
            InputPanel.Instance.OnPointerUpEvent.AddListener(OnPointerUp);
            InputPanel.Instance.OnDragDelta.AddListener(OnDragDelta);
        }

        private void RemoveListeners()
        {
            InputPanel.Instance.OnPointerDownEvent.RemoveAllListeners();
        }
    }
}