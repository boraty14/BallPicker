using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(menuName = "Game Settings/Create Player Settings")]
    public class SPlayerSettings : ScriptableObject
    {
        [PropertyTooltip("Player vertical speed")]public float verticalSpeed;
        [PropertyTooltip("Player horizontal speed")]public float horizontalSpeed;
        [PropertyTooltip("Horizontal limits where player can go")]public float horizontalLimit;
        [PropertyTooltip("Maximum drag in a fixed update")]public float dragLimit;
    }
}
