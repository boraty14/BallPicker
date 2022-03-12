using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Environment;
using GameElements;
using GameSettings;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField] private SLevelManagerSettings levelManagerSettings;
        private List<SLevelSettings> _shuffledLevels = new List<SLevelSettings>();
        private GameObject _previousLevelPrefab;
        private GameObject _currentLevelPrefab;
        private Color _previousGroundColor = Color.white;

        public const string PrefsLevelKey = "Level";

        public int CurrentLevelNeededObject => _shuffledLevels[0].neededObjectCount;

        public float CurrentLevelEndWaitDuration => _shuffledLevels[0].endWaitDuration;

        public float CurrentLevelEndPoint =>
            _currentLevelPrefab.GetComponentInChildren<LevelEndTrigger>().transform.position.z;

        private void OnEnable()
        {
            EventBus.OnTapToPlay += OnTapToPlay;
        }

        private void OnDisable()
        {
            EventBus.OnTapToPlay -= OnTapToPlay;
        }

        private void OnTapToPlay()
        {
            DestroyPreviousLevel();
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
            _previousLevelPrefab = _currentLevelPrefab;
            var levelOffset = GetLevelInstantiateOffset();
            _currentLevelPrefab = Instantiate(_shuffledLevels[0].levelPrefab,
                Vector3.zero + levelOffset * Vector3.forward, Quaternion.identity);
            SetRandomGroundColor();
            EventBus.OnLevelReset?.Invoke();
            yield break;
        }

        private float GetLevelInstantiateOffset()
        {
            if (_previousLevelPrefab == null) return 0f;
            var movingPlatformParent = _previousLevelPrefab.GetComponentInChildren<MovingPlatform>().transform.parent;
            return movingPlatformParent.position.z + movingPlatformParent.localScale.z;
        }

        private void SetRandomGroundColor()
        {
            Color randomGroundColor;
            while (true)
            {
                randomGroundColor = levelManagerSettings.colors[Random.Range(0, levelManagerSettings.colors.Count)];
                if (randomGroundColor != _previousGroundColor) break;
            }
            _currentLevelPrefab.GetComponentInChildren<Ground>().SetRandomColor(randomGroundColor);
            _previousGroundColor = randomGroundColor;
        }

        private void DestroyPreviousLevel()
        {
            if (_previousLevelPrefab != null)
                Destroy(_previousLevelPrefab, levelManagerSettings.destroyPreviousLevelDuration);
        }
    }
}