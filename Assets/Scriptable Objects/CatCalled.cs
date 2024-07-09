using UnityEngine;

namespace Scriptable_Objects
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CatCalled", menuName = "ScriptableObjects/CatCalled", order = 1)]
    public class CatCalled : ScriptableObject
    {
        public bool exists;
    }
}
