using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor
{
    public class LevelEditor : MonoBehaviour
    {
        [Title("Current Level")]
        [SerializeField] private GameObject currentLevel;
        
        [Title("Level Objects")]
        [SerializeField] private GameObject firstPartObject;
        [SerializeField] private GameObject secondPartObject;
        [SerializeField] private GameObject thirdPartObject;

        [Button]
        public void GenerateLevel()
        {
            if (currentLevel != null)
            {
                Debug.LogError("There is already a level in editor");
                return;
            }
            
            
        }

        [Button]
        public void ChangeLevelObjectModels()
        {
            if (firstPartObject != null)
            {
                
            }
        }
    }
}
