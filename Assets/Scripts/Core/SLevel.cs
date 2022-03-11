using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SLevel : ScriptableObject
    {
        public GameObject environment;
        public List<GameObject> firstPartObjects;
        public List<GameObject> secondPartObjects;
        public List<GameObject> thirdPartObjects;
    }
}
