using DG.Tweening;
using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Moving Platform Settings")]
    public class SMovingPlatformSettings : ScriptableObject
    {
        public float moveDuration;
        public Ease moveEase;
        public float moveHeight;
    }
}
