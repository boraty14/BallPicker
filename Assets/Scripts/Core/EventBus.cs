using System;
using UnityEngine;

namespace Core
{
    public static class EventBus
    {
        public static Action OnLevelWin;
        public static Action OnLevelLose;
        public static Action OnLevelReset;
        public static Action OnLevelEndTrigger;
        public static Action OnTapToPlay;
    }
}
