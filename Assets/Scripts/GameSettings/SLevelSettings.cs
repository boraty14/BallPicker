using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Level Settings")]
    public class SLevelSettings : ScriptableObject
    {
        public int neededObjectCount;
        public float endWaitDuration;
        public GameObject levelPrefab;
    }
}
