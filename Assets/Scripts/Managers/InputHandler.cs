using Core;
using UnityEngine;
using Utils;

namespace Managers
{
    public class InputHandler : MonoBehaviourSingleton<InputHandler>
    {

        private int _pointerCount = 0;

        private void Awake()
        {
            _pointerCount = 0;
        }

        private void OnEnable()
        {
            EventBus.OnLevelWin += OnLevelWin;
            EventBus.OnLevelLose += OnLevelLose;
            EventBus.OnLevelReset += OnLevelReset;
        }

        private void OnDisable()
        {
            EventBus.OnLevelWin -= OnLevelWin;
            EventBus.OnLevelLose -= OnLevelLose;
            EventBus.OnLevelReset -= OnLevelReset;
        }
        
        private void OnPointerDown()
        {
            _pointerCount++;
            if(_pointerCount > 1) return;
        }
        private void OnPointerUp()
        {
            _pointerCount--;
            if (_pointerCount > 0) return;
        }
        
        private void OnDragDelta(Vector2 delta)
        {
        }

        private void OnLevelReset()
        {
            AddListeners();
        }

        private void OnLevelWin()
        {
            RemoveListeners();
        }
        
        private void OnLevelLose()
        {
            RemoveListeners();
        }
        

        public void AddListeners()
        {
            InputPanel.Instance.OnPointerDownEvent.AddListener(OnPointerDown);
            InputPanel.Instance.OnPointerUpEvent.AddListener(OnPointerUp);
            InputPanel.Instance.OnDragDelta.AddListener(OnDragDelta);
        }

        public void RemoveListeners()
        {
            InputPanel.Instance.OnPointerDownEvent.RemoveAllListeners();
        }
    }
}