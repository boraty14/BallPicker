using System.Collections;
using System.Collections.Generic;
using Core;
using GameSettings;
using Managers;
using UI;
using UnityEngine;
using Utils;

namespace Level
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField] private SLevelManagerSettings levelManagerSettings;
        private List<SLevelSettings> _shuffledLevels = new List<SLevelSettings>();
        private GameObject _previousLevelPrefab;
        private GameObject _currentLevelPrefab;

        public const string PrefsLevelKey = "Level";

        public int CurrentLevelNeededObject => _shuffledLevels[0].neededObjectCount;

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
        }

        private void OnLevelLose()
        {
        }

        private void DestroyPreviousLevel()
        {
            if (_previousLevelPrefab != null)
                Destroy(_previousLevelPrefab, levelManagerSettings.destroyPreviousLevelDuration);
        }

        private void Start()
        {
            CreateLevelList();
            _currentLevelPrefab = null;
            StartCoroutine(nameof(LevelChangeRoutine));
        }

        private void CreateLevelList()
        {
            _shuffledLevels = new List<SLevelSettings>(levelManagerSettings.levels);

            int level = PlayerPrefs.GetInt(PrefsLevelKey);
            if (level >= _shuffledLevels.Count)
            {
                _shuffledLevels.RemoveAt(0);
                _shuffledLevels.ShuffleList();
            }
            else
            {
                for (int i = 0; i < level; i++)
                {
                    _shuffledLevels.RemoveAt(0);
                }
            }
        }

        public void NextLevel()
        {
            int level = PlayerPrefs.GetInt(PrefsLevelKey) + 1;
            PlayerPrefs.SetInt(PrefsLevelKey, level);

            _shuffledLevels.RemoveAt(0);
            if (_shuffledLevels.Count <= 0)
            {
                CreateLevelList();
            }

            StartCoroutine(nameof(LevelChangeRoutine));
        }


        public void ReplayLevel()
        {
            StartCoroutine(nameof(LevelChangeRoutine));
        }

        private IEnumerator LevelChangeRoutine()
        {
            // if (_currentLevelPrefab != null)
            // {
            //     Destroy(_currentLevelPrefab.gameObject);
            // }
            _previousLevelPrefab = _currentLevelPrefab;
            DestroyPreviousLevel();
            _currentLevelPrefab = Instantiate(_shuffledLevels[0].levelPrefab);
            UIManager.Instance.HidePanel(PanelType.All);
            EventBus.OnLevelReset?.Invoke();
            yield break;
        }
    }
}