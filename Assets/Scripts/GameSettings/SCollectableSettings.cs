using DG.Tweening;
using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Collectable Settings")]
    public class SCollectableSettings : ScriptableObject
    {
        public float destroyDuration;
        public Ease destroyEase;
    }
}
