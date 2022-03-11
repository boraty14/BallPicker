using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressPresenter : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
        private Mover _playerMover;
        private Vector3 _levelStartPoint;
        private Vector3 _levelEndPoint;

        private void OnEnable()
        {
            EventBus.OnLevelReset += OnLevelReset;
            if(_playerMover == null) _playerMover = FindObjectOfType<Mover>();
            _playerMover.OnPlayerMove += OnPlayerMove;
        }

        private void OnDisable()
        {
            EventBus.OnLevelReset -= OnLevelReset;
            _playerMover.OnPlayerMove -= OnPlayerMove;
        }

        private void OnLevelReset()
        {
            
        }

        private void OnPlayerMove(Vector3 playerPosition)
        {
            
        }
    }
}
