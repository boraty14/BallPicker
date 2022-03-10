using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

namespace Managers
{
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [SerializeField] private List<GameObject> levels = new List<GameObject>();
        private List<GameObject> shuffledLevels = new List<GameObject>();
        private GameObject currentLevel;

        public const string PrefsLevelKey = "Level";

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
        
        private void OnLevelReset()
        {
            
        }

        private void Start()
        {
            CreateLevelList();
            currentLevel = null;
            StartCoroutine(nameof(LevelChangeRoutine));
        }

        private void CreateLevelList()
        {
            shuffledLevels = new List<GameObject>(levels);

            int level = PlayerPrefs.GetInt(PrefsLevelKey);
            if (level >= shuffledLevels.Count)
            {
                shuffledLevels.RemoveAt(0);
                shuffledLevels.ShuffleList();
            }
            else
            {
                for (int i = 0; i < level; i++)
                {
                    shuffledLevels.RemoveAt(0);
                }
            }
        }
        
        public void NextLevel()
        {
            int level = PlayerPrefs.GetInt(PrefsLevelKey) + 1;
            PlayerPrefs.SetInt(PrefsLevelKey, level);

            shuffledLevels.RemoveAt(0);
            if (shuffledLevels.Count <= 0)
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
            if (currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            
            currentLevel = Instantiate(shuffledLevels[0]);
            UIManager.Instance.HidePanel(PanelType.All);
            OnLevelReset();
            EventBus.OnLevelReset?.Invoke();
            yield break;
        }
        
    }
}