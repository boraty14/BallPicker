using System.Collections;
using GameElements;
using GameSettings;
using UnityEngine;

namespace Level
{
    public class LevelEndTrigger : MonoBehaviour
    {
        [SerializeField] private SLevelSettings levelSettings;
        [SerializeField] private MovingPlatform movingPlatform;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartCoroutine(CalculateLevelEnd());
        }

        private IEnumerator CalculateLevelEnd()
        {
            yield return new WaitForSeconds(levelSettings.endWaitDuration);
            movingPlatform.CheckLevelEndStatus();
        }
    }
}
