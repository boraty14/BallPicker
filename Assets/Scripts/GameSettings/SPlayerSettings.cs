using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Player Settings")]
    public class SPlayerSettings : ScriptableObject
    {
        public float verticalSpeed;
        public float horizontalSpeed;
        public float horizontalLimit;
        public float dragLimit;
    }
}
