using System;
using DG.Tweening;
using GameSettings;
using UnityEngine;

namespace GameElements
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private SCollectableSettings collectableSettings;
        private bool _didHitPlatform = false;

        public bool HitMovingPlatform()
        {
            if (_didHitPlatform) return false;
            _didHitPlatform = true;
            transform.DOScale(collectableSettings.destroyScale, collectableSettings.destroyDuration)
                .SetEase(collectableSettings.destroyEase).OnComplete(
                    () =>
                    { 
                        Destroy(gameObject);
                    });
            return true;
        }
    }
}
