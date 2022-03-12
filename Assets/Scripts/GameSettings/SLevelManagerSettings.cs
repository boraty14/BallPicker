using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Level Manager Settings")]
    public class SLevelManagerSettings : ScriptableObject
    {
        public List<SLevelSettings> levels = new List<SLevelSettings>();
        [PropertyTooltip("Random ground colors")]public List<Color> colors = new List<Color>();
        public float destroyPreviousLevelDuration;
    }
}
