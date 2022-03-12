using System.Collections.Generic;
using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Level Manager Settings")]
    public class SLevelManagerSettings : ScriptableObject
    {
        public List<SLevelSettings> levels = new List<SLevelSettings>();
        public List<Color> colors = new List<Color>();
        public float destroyPreviousLevelDuration;
    }
}
