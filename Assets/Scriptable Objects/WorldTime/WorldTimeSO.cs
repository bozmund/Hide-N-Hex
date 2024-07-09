using System;
using UnityEngine;


namespace Scriptable_Objects.WorldTime
{
    [Serializable]
    [CreateAssetMenu(fileName = "WorldTimeSO", menuName = "ScriptableObjects/WorldTimeSO", order = 1)]
    public class WorldTimeSo : ScriptableObject
    {
        public TimeSpan TimeSpan;
    }
}