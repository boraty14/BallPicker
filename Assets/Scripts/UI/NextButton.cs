using Level;
using UnityEngine;

namespace UI
{
    public class NextButton : MonoBehaviour
    {
        public void PressNext()
        {
            LevelManager.Instance.NextLevel();
        }
    }
}
