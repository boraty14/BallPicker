using Core;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressPresenter : MonoBehaviour
    {
        private Image _progressBar;
        private Mover _playerMover;
        private float _levelStartPoint;
        private float _levelEndPoint;

        private void Awake()
        {
            _progressBar = GetComponent<Image>();
        }

        private void OnEnable()
        {
            EventBus.OnLevelEndTrigger += OnLevelEndTrigger;
            if(_playerMover == null) _playerMover = FindObjectOfType<Mover>();
            _playerMover.OnPlayerMove += OnPlayerMove;
            SetLevelPoints();
        }

        private void OnDisable()
        {
            EventBus.OnLevelEndTrigger -= OnLevelEndTrigger;
            _playerMover.OnPlayerMove -= OnPlayerMove;
        }
        

        private void SetLevelPoints()
        {
            _levelStartPoint = _playerMover.transform.position.z;
            _levelEndPoint = LevelManager.Instance.CurrentLevelEndPoint;
            UpdateProgress(_levelStartPoint);
        }
        
        private void OnLevelEndTrigger()
        {
            UpdateProgress(_levelEndPoint);
        }

        private void OnPlayerMove(float playerPosition)
        {
            UpdateProgress(playerPosition);
        }

        private void UpdateProgress(float playerPosition)
        {
            _progressBar.fillAmount = (playerPosition - _levelStartPoint) / (_levelEndPoint - _levelStartPoint);
        }
    }
}
