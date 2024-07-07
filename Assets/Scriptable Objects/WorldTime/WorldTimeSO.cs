using System;
using UnityEngine;


namespace Scriptable_Objects.WorldTime
{
    [CreateAssetMenu(fileName = "WorldTimeSO", menuName = "ScriptableObjects/WorldTimeSO", order = 1)]
    public class WorldTimeSO : ScriptableObject
    {
        public TimeSpan TimeSpan = new ();
    }
}