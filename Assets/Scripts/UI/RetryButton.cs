using Level;
using UnityEngine;

namespace UI
{
    public class RetryButton : MonoBehaviour
    {
        public void PressRetry()
        {
            LevelManager.Instance.ReplayLevel();
        }
    }
}
